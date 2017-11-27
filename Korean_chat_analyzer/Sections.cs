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
    class Sections
    {
        int interval; //초단위 //5~10초 적용 예상중...
        ArrayList sectionArray; //각 색션별 정보. (자료형 Section)
        Dictionary<String, int> totalWord; //전체 내용중 단어 발생빈도 정보 (자료형 Dictionary<String,int>)
        Dictionary<String, int> totalPosInfo; // 전체 내용중 품사 발생 빈도 정보 (자료형 Dictionary<String,int>)
        Dictionary<String, int> totalNoun; //전체 내용중 명사 발생빈도 정보 (자료형 Dictionary<String,int>)

        public void GenerateSections(int _interval, ArrayList chatArray) // interval 몇초단위로 끊을것인가, charArray 채팅내용 저장된 Line배열
        {
            interval = _interval;
            sectionArray = new ArrayList();
            totalWord = new Dictionary<string, int>();
            totalPosInfo = new Dictionary<string, int>();
            totalNoun = new Dictionary<string, int>();

            int endLineSec = ((Line)chatArray[chatArray.Count - 1]).hour * 3600 + ((Line)chatArray[chatArray.Count - 1]).min * 60 + ((Line)chatArray[chatArray.Count - 1]).sec;
            for (int i = 0; i <= endLineSec / interval; i++)
            {
                sectionArray.Add(new Section(i));
            }

            int totalSec;
            int charArrayCount = chatArray.Count;
            for (int i = 0; i < charArrayCount; i++)
            {
                Line temp = (Line)chatArray[i];
                totalSec = temp.hour * 3600 + temp.min * 60 + temp.sec;

                ((Section)sectionArray[totalSec / interval]).count++;
                ((Section)sectionArray[totalSec / interval]).addId(temp.name);

                var tokens = TwitterKoreanProcessorCS.Tokenize(temp.chat);
                try
                {
                    tokens = TwitterKoreanProcessorCS.Stem(tokens); //일반형으로 변환
                }
                catch (Exception e)
                {
                    //stem 중에 애러나면 stem 하지않는다... 
                    //왜 애러나는지모름 
                    // ex)'어 왜 벌써 61명남음?'
                    //이라는 문장은 stem이 안된다
                }
                foreach (var token in tokens)
                {
                    ((Section)sectionArray[totalSec / interval]).addToken(token.Text, token.Pos);

                    if (totalWord.ContainsKey(token.Text))
                        totalWord[token.Text]++;
                    else
                        totalWord.Add(token.Text, 1);

                    if (totalPosInfo.ContainsKey(token.Pos.ToString()))
                        totalPosInfo[token.Pos.ToString()]++;
                    else
                        totalPosInfo.Add(token.Pos.ToString(), 1);

                    if (token.Pos == KoreanPos.Noun)
                    {
                        ((Section)sectionArray[totalSec / interval]).noun_count++;
                    }

                    if (token.Pos == KoreanPos.Noun)
                    {
                        if (totalNoun.ContainsKey(token.Text))
                            totalNoun[token.Text]++;
                        else
                            totalNoun.Add(token.Text, 1);
                    }

                    if (token.Text.Equals("ㅋㅋ"))
                    {
                        ((Section)sectionArray[totalSec / interval]).kk_count++;
                    }
                    else if (token.Pos == KoreanPos.Space)
                        ((Section)sectionArray[totalSec / interval]).Space_count++;
                    else if (token.Pos == KoreanPos.KoreanParticle)
                        ((Section)sectionArray[totalSec / interval]).KoreanParticle_count++;
                    else if (token.Pos == KoreanPos.ProperNoun)
                        ((Section)sectionArray[totalSec / interval]).ProperNoun_count++;
                    else if (token.Pos == KoreanPos.Verb)
                        ((Section)sectionArray[totalSec / interval]).Verb_count++;
                    else if (token.Pos == KoreanPos.Josa)
                        ((Section)sectionArray[totalSec / interval]).Josa_count++;
                    else if (token.Pos == KoreanPos.Punctuation)
                        ((Section)sectionArray[totalSec / interval]).Punctuation_count++;
                    else if (token.Pos == KoreanPos.Alpha)
                        ((Section)sectionArray[totalSec / interval]).Alpha_count++;
                    else if (token.Pos == KoreanPos.Number)
                        ((Section)sectionArray[totalSec / interval]).Number_count++;


                }
            }
        }

        public void sections2Text(string filename, string argFileName)
        {
            StreamWriter sw = new StreamWriter(filename);
            StreamWriter csv = new StreamWriter(argFileName + "_output.csv", false, Encoding.Default);
            csv.WriteLine("전체초,시간,채팅참여자수,채팅횟수,'ㅋㅋ'발생횟수,Noun_count,Space_count,KoreanParticle_count,ProperNoun_count,Verb_count,Josa_count,Punctuation_count,Alpha_count,Number_count");
            StreamWriter csv2 = new StreamWriter("temp_for_tensorflow.csv", false, Encoding.Default);
            int sectionArraySize = sectionArray.Count;
            for (int i = 0; i < sectionArraySize; i++)
            {
                Section temp = (Section)sectionArray[i];
                string str = temp.PrintStringAll();
                sw.WriteLine(str);

                csv.Write(temp.getNum() * interval + ",");
                csv.Write(Section.calcSec2Time(temp.getNum() * interval) + ",");
                csv.Write(temp.getIdCount() + ",");
                csv.Write(temp.count + ",");
                csv.Write(temp.kk_count + ",");
                csv.Write(temp.noun_count + ",");

                csv.Write(temp.Space_count + ",");
                csv.Write(temp.KoreanParticle_count + ",");
                csv.Write(temp.ProperNoun_count + ",");
                csv.Write(temp.Verb_count + ",");
                csv.Write(temp.Josa_count + ",");
                csv.Write(temp.Punctuation_count + ",");
                csv.Write(temp.Alpha_count + ",");
                csv.Write(temp.Number_count);
                csv.WriteLine();
                
                csv2.Write(temp.getIdCount() + ",");
                csv2.Write(temp.count + ",");
                csv2.Write(temp.kk_count + ",");
                csv2.Write(temp.noun_count + ",");

                csv2.Write(temp.Space_count + ",");
                csv2.Write(temp.KoreanParticle_count + ",");
                csv2.Write(temp.ProperNoun_count + ",");
                csv2.Write(temp.Verb_count + ",");
                csv2.Write(temp.Josa_count + ",");
                csv2.Write(temp.Punctuation_count + ",");
                csv2.Write(temp.Alpha_count + ",");
                csv2.Write(temp.Number_count);
                csv2.WriteLine();
            }

            sw.Close();
            csv.Close();
            csv2.Close();
        }

        public void totalInfo2Text(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);

            sw.WriteLine("---------단어--------");
            foreach (var temp in totalWord)
            {
                sw.WriteLine(temp.Key + "\t" + temp.Value);
            }
            sw.WriteLine("---------품사--------");
            foreach (var temp in totalPosInfo)
            {
                sw.WriteLine(temp.Key + "\t" + temp.Value);
            }

            sw.Close();
        }

        public void nounCount2Text(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);

            sw.WriteLine("---------명사--------");
            foreach (var temp in totalNoun)
            {
                sw.WriteLine(temp.Key + "\t" + temp.Value);
            }

            sw.Close();
        }



    }
}
