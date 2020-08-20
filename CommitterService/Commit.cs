using CommitterService.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static CommitterService.Model.GeneralDto;
using static CommitterService.Model.Github;

namespace CommitterService
{
    public class Commit
    {
        private readonly HttpClient client = new HttpClient();
        private string CommitBachtFileName = "Batch\\commit.bat";
        private string PushBachtFileName = "Batch\\push.bat";
        private string RepositoryPath = "c:\\Repository\\Committer\\readme.md";
        private string QuoteUrl = "http://extensions.biryudumkitap.com/quote";
        private int BeginDayOfYear = 229;
        private Dictionary<int, Dictionary<int, TodayWord>> MBOZKAYAYearly = new Dictionary<int, Dictionary<int, TodayWord>>
            {
                {0, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=3,Word=""} },
                        {2, new TodayWord{CommitCount=3,Word=""} },
                        {3, new TodayWord{CommitCount=3,Word=""} },
                        {4, new TodayWord{CommitCount=3,Word=""} },
                        {5, new TodayWord{CommitCount=3,Word=""} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {1,new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="M"} },
                        {2, new TodayWord{CommitCount=9,Word="M"} },
                        {3, new TodayWord{CommitCount=9,Word="M"} },
                        {4, new TodayWord{CommitCount=9,Word="M"} },
                        {5, new TodayWord{CommitCount=9,Word="M"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {2,new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="M"} },
                        {2, new TodayWord{CommitCount=3,Word="M"} },
                        {3, new TodayWord{CommitCount=3,Word="M"} },
                        {4, new TodayWord{CommitCount=3,Word="M"} },
                        {5, new TodayWord{CommitCount=3,Word="M"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {3,new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=3,Word="M"} },
                        {2, new TodayWord{CommitCount=9,Word="M"} },
                        {3, new TodayWord{CommitCount=3,Word="M"} },
                        {4, new TodayWord{CommitCount=3,Word="M"} },
                        {5, new TodayWord{CommitCount=3,Word="M"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {4,new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=3,Word="M"} },
                        {2, new TodayWord{CommitCount=9,Word="M"} },
                        {3, new TodayWord{CommitCount=3,Word="M"} },
                        {4, new TodayWord{CommitCount=3,Word="M"} },
                        {5, new TodayWord{CommitCount=3,Word="M"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {5,new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="M"} },
                        {2, new TodayWord{CommitCount=3,Word="M"} },
                        {3, new TodayWord{CommitCount=3,Word="M"} },
                        {4, new TodayWord{CommitCount=3,Word="M"} },
                        {5, new TodayWord{CommitCount=3,Word="M"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {6,new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="M"} },
                        {2, new TodayWord{CommitCount=9,Word="M"} },
                        {3, new TodayWord{CommitCount=9,Word="M"} },
                        {4, new TodayWord{CommitCount=9,Word="M"} },
                        {5, new TodayWord{CommitCount=9,Word="M"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {7, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=3,Word=""} },
                        {2, new TodayWord{CommitCount=3,Word=""} },
                        {3, new TodayWord{CommitCount=3,Word=""} },
                        {4, new TodayWord{CommitCount=3,Word=""} },
                        {5, new TodayWord{CommitCount=3,Word=""} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {8,new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="B"} },
                        {2, new TodayWord{CommitCount=9,Word="B"} },
                        {3, new TodayWord{CommitCount=9,Word="B"} },
                        {4, new TodayWord{CommitCount=9,Word="B"} },
                        {5, new TodayWord{CommitCount=9,Word="B"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {9,new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="B"} },
                        {2, new TodayWord{CommitCount=3,Word="B"} },
                        {3, new TodayWord{CommitCount=9,Word="B"} },
                        {4, new TodayWord{CommitCount=3,Word="B"} },
                        {5, new TodayWord{CommitCount=9,Word="B"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {10,new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="B"} },
                        {2, new TodayWord{CommitCount=3,Word="B"} },
                        {3, new TodayWord{CommitCount=9,Word="B"} },
                        {4, new TodayWord{CommitCount=3,Word="B"} },
                        {5, new TodayWord{CommitCount=9,Word="B"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {11,new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="B"} },
                        {2, new TodayWord{CommitCount=9,Word="B"} },
                        {3, new TodayWord{CommitCount=9,Word="B"} },
                        {4, new TodayWord{CommitCount=9,Word="B"} },
                        {5, new TodayWord{CommitCount=9,Word="B"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {12, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=3,Word=""} },
                        {2, new TodayWord{CommitCount=3,Word=""} },
                        {3, new TodayWord{CommitCount=3,Word=""} },
                        {4, new TodayWord{CommitCount=3,Word=""} },
                        {5, new TodayWord{CommitCount=3,Word=""} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {13,new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="O"} },
                        {2, new TodayWord{CommitCount=9,Word="O"} },
                        {3, new TodayWord{CommitCount=9,Word="O"} },
                        {4, new TodayWord{CommitCount=9,Word="O"} },
                        {5, new TodayWord{CommitCount=9,Word="O"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {14,new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="O"} },
                        {2, new TodayWord{CommitCount=3,Word="O"} },
                        {3, new TodayWord{CommitCount=3,Word="O"} },
                        {4, new TodayWord{CommitCount=3,Word="O"} },
                        {5, new TodayWord{CommitCount=9,Word="O"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {15,new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="O"} },
                        {2, new TodayWord{CommitCount=3,Word="O"} },
                        {3, new TodayWord{CommitCount=3,Word="O"} },
                        {4, new TodayWord{CommitCount=3,Word="O"} },
                        {5, new TodayWord{CommitCount=9,Word="O"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {16,new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="O"} },
                        {2, new TodayWord{CommitCount=3,Word="O"} },
                        {3, new TodayWord{CommitCount=3,Word="O"} },
                        {4, new TodayWord{CommitCount=3,Word="O"} },
                        {5, new TodayWord{CommitCount=9,Word="O"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {17,new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="O"} },
                        {2, new TodayWord{CommitCount=9,Word="O"} },
                        {3, new TodayWord{CommitCount=9,Word="O"} },
                        {4, new TodayWord{CommitCount=9,Word="O"} },
                        {5, new TodayWord{CommitCount=9,Word="O"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {18, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=3,Word=""} },
                        {2, new TodayWord{CommitCount=3,Word=""} },
                        {3, new TodayWord{CommitCount=3,Word=""} },
                        {4, new TodayWord{CommitCount=3,Word=""} },
                        {5, new TodayWord{CommitCount=3,Word=""} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {19, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="Z"} },
                        {2, new TodayWord{CommitCount=3,Word="Z"} },
                        {3, new TodayWord{CommitCount=3,Word="Z"} },
                        {4, new TodayWord{CommitCount=3,Word="Z"} },
                        {5, new TodayWord{CommitCount=9,Word="Z"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {20, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="Z"} },
                        {2, new TodayWord{CommitCount=3,Word="Z"} },
                        {3, new TodayWord{CommitCount=3,Word="Z"} },
                        {4, new TodayWord{CommitCount=9,Word="Z"} },
                        {5, new TodayWord{CommitCount=9,Word="Z"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {21, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="Z"} },
                        {2, new TodayWord{CommitCount=3,Word="Z"} },
                        {3, new TodayWord{CommitCount=9,Word="Z"} },
                        {4, new TodayWord{CommitCount=3,Word="Z"} },
                        {5, new TodayWord{CommitCount=9,Word="Z"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {22, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="Z"} },
                        {2, new TodayWord{CommitCount=9,Word="Z"} },
                        {3, new TodayWord{CommitCount=3,Word="Z"} },
                        {4, new TodayWord{CommitCount=3,Word="Z"} },
                        {5, new TodayWord{CommitCount=9,Word="Z"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {23, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="Z"} },
                        {2, new TodayWord{CommitCount=3,Word="Z"} },
                        {3, new TodayWord{CommitCount=3,Word="Z"} },
                        {4, new TodayWord{CommitCount=3,Word="Z"} },
                        {5, new TodayWord{CommitCount=9,Word="Z"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {24, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=3,Word=""} },
                        {2, new TodayWord{CommitCount=3,Word=""} },
                        {3, new TodayWord{CommitCount=3,Word=""} },
                        {4, new TodayWord{CommitCount=3,Word=""} },
                        {5, new TodayWord{CommitCount=3,Word=""} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {25, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="K"} },
                        {2, new TodayWord{CommitCount=9,Word="K"} },
                        {3, new TodayWord{CommitCount=9,Word="K"} },
                        {4, new TodayWord{CommitCount=9,Word="K"} },
                        {5, new TodayWord{CommitCount=9,Word="K"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {26, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=3,Word="K"} },
                        {2, new TodayWord{CommitCount=3,Word="K"} },
                        {3, new TodayWord{CommitCount=9,Word="K"} },
                        {4, new TodayWord{CommitCount=3,Word="K"} },
                        {5, new TodayWord{CommitCount=3,Word="K"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {27, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=3,Word="K"} },
                        {2, new TodayWord{CommitCount=9,Word="K"} },
                        {3, new TodayWord{CommitCount=3,Word="K"} },
                        {4, new TodayWord{CommitCount=9,Word="K"} },
                        {5, new TodayWord{CommitCount=3,Word="K"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {28, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="K"} },
                        {2, new TodayWord{CommitCount=3,Word="K"} },
                        {3, new TodayWord{CommitCount=3,Word="K"} },
                        {4, new TodayWord{CommitCount=3,Word="K"} },
                        {5, new TodayWord{CommitCount=9,Word="K"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {29, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=3,Word=""} },
                        {2, new TodayWord{CommitCount=3,Word=""} },
                        {3, new TodayWord{CommitCount=3,Word=""} },
                        {4, new TodayWord{CommitCount=3,Word=""} },
                        {5, new TodayWord{CommitCount=3,Word=""} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {30, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="A"} },
                        {2, new TodayWord{CommitCount=9,Word="A"} },
                        {3, new TodayWord{CommitCount=9,Word="A"} },
                        {4, new TodayWord{CommitCount=9,Word="A"} },
                        {5, new TodayWord{CommitCount=9,Word="A"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {31, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="A"} },
                        {2, new TodayWord{CommitCount=3,Word="A"} },
                        {3, new TodayWord{CommitCount=9,Word="A"} },
                        {4, new TodayWord{CommitCount=3,Word="A"} },
                        {5, new TodayWord{CommitCount=3,Word="A"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {32, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="A"} },
                        {2, new TodayWord{CommitCount=3,Word="A"} },
                        {3, new TodayWord{CommitCount=9,Word="A"} },
                        {4, new TodayWord{CommitCount=3,Word="A"} },
                        {5, new TodayWord{CommitCount=3,Word="A"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                 {33, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="A"} },
                        {2, new TodayWord{CommitCount=9,Word="A"} },
                        {3, new TodayWord{CommitCount=9,Word="A"} },
                        {4, new TodayWord{CommitCount=9,Word="A"} },
                        {5, new TodayWord{CommitCount=9,Word="A"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                 {34, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=3,Word=""} },
                        {2, new TodayWord{CommitCount=3,Word=""} },
                        {3, new TodayWord{CommitCount=3,Word=""} },
                        {4, new TodayWord{CommitCount=3,Word=""} },
                        {5, new TodayWord{CommitCount=3,Word=""} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                 {35, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="Y"} },
                        {2, new TodayWord{CommitCount=3,Word="Y"} },
                        {3, new TodayWord{CommitCount=3,Word="Y"} },
                        {4, new TodayWord{CommitCount=3,Word="Y"} },
                        {5, new TodayWord{CommitCount=3,Word="Y"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                 {36, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=3,Word="Y"} },
                        {2, new TodayWord{CommitCount=9,Word="Y"} },
                        {3, new TodayWord{CommitCount=3,Word="Y"} },
                        {4, new TodayWord{CommitCount=3,Word="Y"} },
                        {5, new TodayWord{CommitCount=3,Word="Y"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                 {37, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=3,Word="Y"} },
                        {2, new TodayWord{CommitCount=3,Word="Y"} },
                        {3, new TodayWord{CommitCount=9,Word="Y"} },
                        {4, new TodayWord{CommitCount=9,Word="Y"} },
                        {5, new TodayWord{CommitCount=9,Word="Y"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                  {38, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=3,Word="Y"} },
                        {2, new TodayWord{CommitCount=9,Word="Y"} },
                        {3, new TodayWord{CommitCount=3,Word="Y"} },
                        {4, new TodayWord{CommitCount=3,Word="Y"} },
                        {5, new TodayWord{CommitCount=3,Word="Y"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                  {39, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="Y"} },
                        {2, new TodayWord{CommitCount=3,Word="Y"} },
                        {3, new TodayWord{CommitCount=3,Word="Y"} },
                        {4, new TodayWord{CommitCount=3,Word="Y"} },
                        {5, new TodayWord{CommitCount=3,Word="Y"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                  {40, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=3,Word=""} },
                        {2, new TodayWord{CommitCount=3,Word=""} },
                        {3, new TodayWord{CommitCount=3,Word=""} },
                        {4, new TodayWord{CommitCount=3,Word=""} },
                        {5, new TodayWord{CommitCount=3,Word=""} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                  {41, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="A"} },
                        {2, new TodayWord{CommitCount=9,Word="A"} },
                        {3, new TodayWord{CommitCount=9,Word="A"} },
                        {4, new TodayWord{CommitCount=9,Word="A"} },
                        {5, new TodayWord{CommitCount=9,Word="A"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {42, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="A"} },
                        {2, new TodayWord{CommitCount=3,Word="A"} },
                        {3, new TodayWord{CommitCount=9,Word="A"} },
                        {4, new TodayWord{CommitCount=3,Word="A"} },
                        {5, new TodayWord{CommitCount=3,Word="A"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                {43, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="A"} },
                        {2, new TodayWord{CommitCount=3,Word="A"} },
                        {3, new TodayWord{CommitCount=9,Word="A"} },
                        {4, new TodayWord{CommitCount=3,Word="A"} },
                        {5, new TodayWord{CommitCount=3,Word="A"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                 {44, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=9,Word="A"} },
                        {2, new TodayWord{CommitCount=9,Word="A"} },
                        {3, new TodayWord{CommitCount=9,Word="A"} },
                        {4, new TodayWord{CommitCount=9,Word="A"} },
                        {5, new TodayWord{CommitCount=9,Word="A"} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                 {45, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=3,Word=""} },
                        {2, new TodayWord{CommitCount=3,Word=""} },
                        {3, new TodayWord{CommitCount=3,Word=""} },
                        {4, new TodayWord{CommitCount=3,Word=""} },
                        {5, new TodayWord{CommitCount=3,Word=""} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                 {46, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=3,Word=""} },
                        {2, new TodayWord{CommitCount=3,Word=""} },
                        {3, new TodayWord{CommitCount=3,Word=""} },
                        {4, new TodayWord{CommitCount=3,Word=""} },
                        {5, new TodayWord{CommitCount=3,Word=""} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                 {47, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=3,Word=""} },
                        {2, new TodayWord{CommitCount=3,Word=""} },
                        {3, new TodayWord{CommitCount=3,Word=""} },
                        {4, new TodayWord{CommitCount=3,Word=""} },
                        {5, new TodayWord{CommitCount=3,Word=""} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                 {48, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=3,Word=""} },
                        {2, new TodayWord{CommitCount=3,Word=""} },
                        {3, new TodayWord{CommitCount=3,Word=""} },
                        {4, new TodayWord{CommitCount=3,Word=""} },
                        {5, new TodayWord{CommitCount=3,Word=""} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                 {49, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=3,Word=""} },
                        {2, new TodayWord{CommitCount=3,Word=""} },
                        {3, new TodayWord{CommitCount=3,Word=""} },
                        {4, new TodayWord{CommitCount=3,Word=""} },
                        {5, new TodayWord{CommitCount=3,Word=""} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                 {50, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=3,Word=""} },
                        {2, new TodayWord{CommitCount=3,Word=""} },
                        {3, new TodayWord{CommitCount=3,Word=""} },
                        {4, new TodayWord{CommitCount=3,Word=""} },
                        {5, new TodayWord{CommitCount=3,Word=""} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
                 {51, new Dictionary<int, TodayWord>
                    {
                        {0, new TodayWord{CommitCount=3,Word=""} },
                        {1, new TodayWord{CommitCount=3,Word=""} },
                        {2, new TodayWord{CommitCount=3,Word=""} },
                        {3, new TodayWord{CommitCount=3,Word=""} },
                        {4, new TodayWord{CommitCount=3,Word=""} },
                        {5, new TodayWord{CommitCount=3,Word=""} },
                        {6, new TodayWord{CommitCount=3,Word=""} },
                    }
                },
            };
        private ISlackService _serviceSlack;
        public Commit(ISlackService serviceSlack)
        {
            _serviceSlack = serviceSlack;

            Init();
        }

        public async void Init()
        {
            try
            {
                int pastCommitCount = await CheckGitHub();
                var todayWord = GetCommitCount(DateTime.Now);

                if (pastCommitCount <= todayWord.CommitCount)
                {
                    for (int i = pastCommitCount; i < todayWord.CommitCount; i++)
                    {
                        if (i == pastCommitCount)
                        {
                            await AppendChanges($"{Environment.NewLine} #### {todayWord.Week}. Hafta {todayWord.Day + 1}. Gün {(todayWord.Word != "" ? string.Concat(todayWord.Word, " Harfi Oluşturuluyor.") : "")}");
                        }
                        else
                        {
                            await AppendChanges();
                        }
                        CommitChanges($"{todayWord.Week}.-Hafta-{todayWord.Day + 1}.-Gün-{(todayWord.Word != "" ? string.Concat(todayWord.Word, "Harfi_Oluşturuluyor.") : "")}-{i + 1}.-Commit");
                    }
                    Push();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private string GetApplicationRoot()
        {
            var exePath = Path.GetDirectoryName(System.Reflection
                              .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return appRoot;
        }

        public void CommitChanges(string commitNote)
        {
            _serviceSlack.WriteMessage($"Current Directory {Directory.GetCurrentDirectory()}");
            _serviceSlack.WriteMessage($"App Root {GetApplicationRoot()}");

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = $"{GetApplicationRoot()}\\{CommitBachtFileName}";
            proc.StartInfo.WorkingDirectory = $"{Directory.GetCurrentDirectory()}";
            //proc.StartInfo.Arguments = "start /wait commit.bat ";
            proc.StartInfo.Arguments = commitNote;
            proc.Start();

        }

        public void Push()
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = $"{GetApplicationRoot()}\\{PushBachtFileName}";
            proc.StartInfo.WorkingDirectory = $"{Directory.GetCurrentDirectory()}";
            proc.Start();

        }

        public async Task<bool> AppendChanges(string head = "")
        {
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(QuoteUrl);

            BirYudumKitap quote = JsonConvert.DeserializeObject<BirYudumKitap>(response);

            string text = $"{Environment.NewLine}{(head != "" ? string.Concat(head, Environment.NewLine) : "")} {quote.quote} -__*{quote.source}*__ {DateTime.Now.ToLocalTime()} {Environment.NewLine}";

            File.AppendAllText(Path.Combine(RepositoryPath), text);

            return true;
        }

        public TodayWord GetCommitCount(DateTime date)
        {
            int remainingDay = date.DayOfYear - BeginDayOfYear;
            if (remainingDay < 0)
            {
                remainingDay = 365 - BeginDayOfYear + date.DayOfYear;
            }

            int week = remainingDay / 7;

            Dictionary<int, TodayWord> MBOZKAYAWeekly = MBOZKAYAYearly[week];
            TodayWord MBOZKAYADaily = MBOZKAYAWeekly[(int)date.DayOfWeek];
            MBOZKAYADaily.Day = remainingDay;
            MBOZKAYADaily.Week = week + 1;

            return MBOZKAYADaily;
        }

        public async Task<int> CheckGitHub()
        {
            int commitCount = 0;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = client.GetStringAsync("https://api.github.com/users/mbozkaya/events");

            var msg = await stringTask;
            List<GetEvent> deserialized = JsonConvert.DeserializeObject<List<GetEvent>>(msg);

            var pushEvents = deserialized.Where(w => w.type == "PushEvent" && w.CreatedDate.Date == DateTime.Now.Date).ToList();

            foreach (var push in pushEvents)
            {
                commitCount += push.payload.commits.Count;
            }

            return commitCount;
        }

    }
}
