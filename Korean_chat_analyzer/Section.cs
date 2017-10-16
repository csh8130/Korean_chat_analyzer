using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moda.Korean.TwitterKoreanProcessorCS;

namespace koran_chat_analyzer
{
    class Section
    {
        int num; // totalSec / interval = num;
        Dictionary<string, MyToken> dict; // key , value == clount

        public int count; //채팅 발생 량
        Dictionary<string, int> id_dict; //채팅 참여자 수
        public int kk_count; //ㅋㅋ발생 수

        public int noun_count; //발생 명사 단어 수

        public int Space_count;
        public int KoreanParticle_count;
        public int ProperNoun_count;
        public int Verb_count;
        public int Josa_count;
        public int Punctuation_count;
        public int Alpha_count;
        public int Number_count;


        public Section(int n)
        {
            num = n;
            dict = new Dictionary<string, MyToken>();
            count = 0;
            id_dict = new Dictionary<string, int>();
        }

        public static string calcSec2Time(int tick)
        {
            int hour;
            int min;
            int sec;
            sec = tick % 60;
            min = tick / 60;
            hour = min / 60;
            min = min % 60;
            string temp = string.Format("{0:D2}:{1:D2}:{2:D2}", hour, min, sec);
            return temp;
        }

        public int getNum()
        {
            return num;
        }

        public void addToken(string str, KoreanPos p)
        {
            if (dict.ContainsKey(str))
                dict[str].count++;
            else
            {
                dict.Add(str, new MyToken(p));
            }
        }

        public void addId(string id)
        {
            if (id_dict.ContainsKey(id))
            {
                id_dict[id]++;
            }
            else
            {
                id_dict.Add(id, 1);
            }
        }

        public int getIdCount() //색션 내에서 채팅에 참여한 참여자 수
        {
            return id_dict.Count;
        }


        public string PrintStringAll()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}번째 Section", num);
            sb.AppendLine();
            foreach (var s in dict)
            {
                sb.AppendFormat("단어:{0}/품사:{1}/{2}번 발생", s.Key, s.Value.pos, s.Value.count);
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
