using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Linq;

namespace AdditionCalculator.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {
            this.ValA = new ReactiveProperty<string>().SetValidateAttribute(() => this.ValA);
            this.ValB = new ReactiveProperty<string>().SetValidateAttribute(() => this.ValB);
            this.ErrMsg = new[]
            { 
                // エラーを束ねて 
                this.ValA.ObserveErrorChanged,
                this.ValB.ObserveErrorChanged
            }
            .CombineLatest(x =>
            {
                // null(エラーなし）を省いて 
                var r = x.Where(y => y != null)
                    // 最初のエラーを返す 
                    .Select(y => y.OfType<string>())
                    .FirstOrDefault(y => y.Any());
                // 無ければ無し、エラーがあれば最初のものを返す 
                return r == null ? "ありません" : r.FirstOrDefault();
            })
            .ToReactiveProperty();

        }

        [Required(ErrorMessage = "その１ 入力してください")]
        [RegularExpression(@"^\d{1,3}$",
            ErrorMessage = "その１は整数3桁の範囲で入力してください。")]
        public ReactiveProperty<string> ValA { get; set; } //= new ReactiveProperty<int>();

        [Required(ErrorMessage = "その2 入力してください")]
        public ReactiveProperty<string> ValB { get; set; } // new ReactiveProperty<int>();
        public ReactiveProperty<int> ValC { get; set; } = new ReactiveProperty<int>();

        public ReactiveProperty<string> ErrMsg { get; set; } // = new ReactiveProperty<string>();

        private DelegateCommand addCmd;
        public DelegateCommand AddCommand =>
            addCmd ?? (addCmd = new DelegateCommand(ExecuteAddCommand, CanExecuteAddCommand));
        void ExecuteAddCommand()
        {
            ValC.Value = int.Parse(ValA.Value) + int.Parse(ValB.Value);
        }

        bool CanExecuteAddCommand()
        {
            return true;
        }
    }
}
