using System;
using System.Collections.Generic;
using System.Text;

namespace csv_benchmark
{
    public static class CSV
    {
        private static char SKIP_CHAR = '"';
        private static char SEPARATOR = ';';
        private static char LINE_BREAK = '\n';

        public class CSVObject
        {
            public Dictionary<string, string>[] Rows { get; set; }
        }

        private static CSVObject ConvertStringsToObject(string[][] strings)
        {
            var headers = strings[0];
            var obj = new CSVObject();
            obj.Rows = new Dictionary<string, string>[strings.Length - 1];
            for(int i = 1; i < strings.Length; i ++)
            {
                obj.Rows[i-1] = new Dictionary<string, string>();
                for(int j = 0; j < headers.Length; j++)
                {
                    var header = headers[j];
                    if (string.IsNullOrEmpty(header)) continue;
                    obj.Rows[i - 1].Add(header, strings[i][j]);
                }
            }

            return obj;
        }

        public static CSVObject ProcessCSV(string csv)
        {
            var isSkipping = false;
            var buffer = new StringBuilder();
            var currentRow = new List<string>();
            var rows = new List<string[]>();

            foreach(char c in csv)
            {
                if (isSkipping)
                {
                    if(c == SKIP_CHAR)
                    {
                        isSkipping = false;
                    }
                    else
                    {
                        buffer.Append(c);
                    }
                } 
                else
                {
                    if(c == SKIP_CHAR)
                    {
                        isSkipping = true;
                    }
                    else
                    {
                        if(c == SEPARATOR)
                        {
                            currentRow.Add(buffer.ToString());
                            buffer = new StringBuilder();
                        }
                        else
                        {
                            if(c == LINE_BREAK)
                            {
                                if(buffer.Length > 0)
                                {
                                    currentRow.Add(buffer.ToString());
                                    buffer = new StringBuilder();
                                }

                                rows.Add(currentRow.ToArray());
                                currentRow = new List<string>();
                            }
                            else
                            {
                                buffer.Append(c);
                            }
                        }
                    }
                }
            }

            if(buffer.Length > 0)
            {
                currentRow.Add(buffer.ToString());
            }

            if(currentRow.Count > 0)
            {
                rows.Add(currentRow.ToArray());
            }

            return ConvertStringsToObject(rows.ToArray());
        }

    }
}
