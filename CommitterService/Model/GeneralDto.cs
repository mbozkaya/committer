using System;
using System.Collections.Generic;
using System.Text;

namespace CommitterService.Model
{
    public class GeneralDto
    {
        public class TodayWord
        {
            public string Word { get; set; }
            public int CommitCount { get; set; }
            public int Day { get; set; }
            public int Week { get; set; }
        }
    }
}
