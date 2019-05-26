using Prism.Commands;
using Prism.Mvvm;

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

        }

        private int valA = 0;
        public int ValA
        {
            get { return valA; }
            set { SetProperty(ref valA, value); }
        }

        private int valB = 0;
        public int ValB
        {
            get { return valB; }
            set { SetProperty(ref valB, value); }
        }

        private int valC = 0;
        public int ValC
        {
            get { return valC; }
            set { SetProperty(ref valC, value); }
        }

        private DelegateCommand addCmd;
        public DelegateCommand AddCommand =>
            addCmd ?? (addCmd = new DelegateCommand(ExecuteAddCommand, CanExecuteAddCommand));
        void ExecuteAddCommand()
        {
            ValC = ValA + ValB;
        }

        bool CanExecuteAddCommand()
        {
            return true;
        }
    }
}
