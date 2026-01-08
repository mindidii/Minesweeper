using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperProject.Models
{
    public class Cell : ViewModels.ViewModelBase
    {
        public int Row { get; }
        public int Col { get; }
        public bool IsMine { get; set; }
        public int NeighborMineCount { get; set; }

        private bool _isOpened;
        public bool IsOpened { get => _isOpened; set => SetProperty(ref _isOpened, value); }

        private bool _isFlagged;
        public bool IsFlagged { get => _isFlagged; set => SetProperty(ref _isFlagged, value); }

        public Cell(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}