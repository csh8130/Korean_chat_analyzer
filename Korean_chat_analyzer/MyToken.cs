using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moda.Korean.TwitterKoreanProcessorCS;

namespace koran_chat_analyzer
{
    class MyToken
    {
        public MyToken(KoreanPos po)
        {
            pos = po;
            count = 1;
        }
        public KoreanPos pos; //명사 형용사.... 같은 pos정보
        public int count;
    }
}
