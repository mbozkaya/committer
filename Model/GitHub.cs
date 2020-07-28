﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace Commiter.Model
{
    public class GitHub
    {
        public class GetEvent
        {
            public long id { get; set; }
            public string type { get; set; }
            public object actor { get; set; }
            public object repo { get; set; }
            public Payload payload { get; set; } = new Payload();
            public string created_at
            {
                get;
                set;
            }
            public DateTime CreatedDate
            {
                get
                {
                    if (string.IsNullOrEmpty(created_at))
                    {
                        return DateTime.Now;
                    }

                    return DateTime.Parse(created_at, null, System.Globalization.DateTimeStyles.RoundtripKind);
                }
                set
                {
                    CreatedDate = value;
                }
            }
        }

        public class Payload
        {
            public long push_id { get; set; }
            public int size { get; set; }
            public int distinct_size { get; set; }
            public string Ref { get; set; }
            public string head { get; set; }
            public string before { get; set; }
            public List<Commit> commits { get; set; } = new List<Commit>();

        }
        public class Commit
        {
            public string sha { get; set; }
            public string message { get; set; }
            public string url { get; set; }
            public bool distinct { get; set; }
            public Author author { get; set; } = new Author();
        }

        public class Author
        {
            public string name { get; set; }
            public string email { get; set; }
        }
    }

    public class BirYudumKitap
    {
        public int id { get; set; }
        public string source { get; set; }
        public string quote { get; set; }
        public string random { get; set; }
    }

    public class TodayWord
    {
        public string Word { get; set; }
        public int CommitCount { get; set; }
        public int Day { get; set; }
        public int Week { get; set; }
    }
}