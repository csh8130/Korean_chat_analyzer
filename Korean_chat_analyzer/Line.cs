using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace koran_chat_analyzer
{
    class Line //채팅 한줄 한줄에 대한 정보 저장
    {
        public int hour;
        public int min;
        public int sec;

        public string name; //id or 닉네임
        public string chat; //채팅내용

        public Line(int h, int m, int s, string n, string c)
        {
            hour = h;
            min = m;
            sec = s;
            name = n;
            chat = c;
        }
        public override string ToString()
        {
            return string.Format("[{0:D2}:{1:D2}:{2:D2}] {3} : {4}", hour, min, sec, name, chat);
        }

    }
}
