using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moda.Korean.TwitterKoreanProcessorCS;

namespace koran_chat_analyzer
{
    class Program
    {
        static ArrayList chatLineArray;

        static void ReadFile(string filename)
        {
            try
            {
                StreamReader sr = new StreamReader(filename);
                while (sr.Peek() >= 0)
                {
                    string strLine = sr.ReadLine();

                    string time = strLine.Substring(0, 14);
                    string noTimeStr = strLine.Substring(strLine.IndexOf(' '));

                    string name = noTimeStr.Substring(1, noTimeStr.IndexOf(':') - 1);
                    string chat = noTimeStr.Substring(noTimeStr.IndexOf(':') + 2);
                    chat = TwitterKoreanProcessorCS.Normalize(chat);
                    //normalize ex) 재밌닼ㅋㅋㅋㅋ -> 재밌다ㅋㅋ ('ㅋ'갯수를 두개로 줄여줌, 'ㅋ'이 받침에 들어간걸 없애줌)

                    int hour = int.Parse(time.Substring(1, 2));
                    int min = int.Parse(time.Substring(4, 2));
                    int sec = int.Parse(time.Substring(7, 2));

                    Line chatLine = new Line(hour, min, sec, name, chat);

                    chatLineArray.Add(chatLine);
                }
                sr.Close();
            }

            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }


            //for(int i=0;i< chatLineArray.Count;i++)
            //{
            //    Console.WriteLine(chatLineArray[i].ToString());
            //}
        }

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {

                Console.WriteLine("사용법 : korean_chat_analyzer.exe 채팅파일.txt");
                Console.WriteLine("[00:00:00.078] 닉네임: 채팅내용");
                Console.WriteLine("으로 이루어진 텍스트문서를 입력파일로 넣어야합니다.");
                Console.WriteLine("프로젝트에 포함된 example_data.txt 를 참고하시오");
                Console.WriteLine("");
                Console.WriteLine("usage : korean_chat_analyzer.exe chat_file.txt");
                Console.WriteLine("");
                return;
            }

            Console.WriteLine("analysing... " + args[0]);

            chatLineArray = new ArrayList();
            string fileName = args[0];
            //fileName = Console.ReadLine();
            ReadFile(fileName);

            Sections sections = new Sections();
            sections.GenerateSections(5, chatLineArray);
            sections.sections2Text(args[0] + "_chat_info.txt", args[0]);
            Console.WriteLine(args[0] + "_chat_info.txt" + " 5초단위로 발생한 형태소 및 빈도수");
            Console.WriteLine(args[0] + "_output.csv" + " 딥러닝에 활용하기 위한 각종 정보");

            sections.totalInfo2Text(args[0] + "_count_info.txt");
            Console.WriteLine(args[0] + "_count_info.txt" + " 전체구간에서의 형태소별 발생 빈도수 / 전체구간에서의 품사별 발생 빈도수");

            sections.nounCount2Text(args[0] + "_noun_count_info.txt");
            Console.WriteLine(args[0] + "_noun_count_info.txt" + " 전체구간에서 명사만 뽑아낸 후 빈도수 측정");
            Console.WriteLine("temp_for_tensorflow.csv" + "_output.csv에서 한글과 시간정보를 제외한 순수한 딥러닝학습용 데이터");

            Console.WriteLine("Done");
        }
    }
}