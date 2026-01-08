using System;
using System.Collections.Generic;

namespace MinesweeperProject.Models
{
    public class SaveData
    {
        public string Nickname { get; set; } = string.Empty; //
        public int Rows { get; set; }
        public int Cols { get; set; }
        public int CurrentTime { get; set; }
        public int MineCount { get; set; }
        public List<CellSaveState> Cells { get; set; } = new(); //
        public string DifficultyName { get; set; } = string.Empty;
    }

    public class CellSaveState
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public bool IsMine { get; set; }
        public bool IsOpened { get; set; }
        public bool IsFlagged { get; set; }
        public int NeighborMineCount { get; set; }
        public int CurrentTime { get; set; }
    }
}
