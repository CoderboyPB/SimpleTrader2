using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.ViewModels
{
    public class MajorIndexListingViewModel : ViewModelBase
    {
        private readonly IMajorIndexService majorIndexService;
        public MajorIndexListingViewModel(IMajorIndexService majorIndexService)
        {
            this.majorIndexService = majorIndexService;
        }

        private MajorIndex dowjones;        

        public MajorIndex DowJones
        {
            get { return dowjones; }
            set { dowjones = value; OnPropertyChanged("DowJones"); }
        }

        private MajorIndex nasdaq;

        public MajorIndex Nasdaq
        {
            get { return nasdaq; }
            set { nasdaq = value; OnPropertyChanged("Nasdaq"); }
        }

        private MajorIndex sp500;

        public MajorIndex SP500
        {
            get { return sp500; }
            set { sp500 = value; OnPropertyChanged("SP500"); }
        }

        private void LoadMajorIndexes()
        {
            majorIndexService.GetMajorIndex(MajorIndexType.DowJones).ContinueWith(task =>
            {
                if(task.Exception == null)
                {
                    DowJones = task.Result;
                }
            });
            majorIndexService.GetMajorIndex(MajorIndexType.Nasdaq).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    Nasdaq = task.Result;
                }
            });
            majorIndexService.GetMajorIndex(MajorIndexType.SP500).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    SP500 = task.Result;
                }
            });
        }

        public static MajorIndexListingViewModel LoadMajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            MajorIndexListingViewModel majorIndexViewModel = new MajorIndexListingViewModel(majorIndexService);
            majorIndexViewModel.LoadMajorIndexes();
            return majorIndexViewModel;
        }
    }
}
