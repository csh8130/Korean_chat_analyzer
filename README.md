# Korean_chat_analyzer
한글 채팅 분석기_ 트위치_아프리카_카카오TV





무슨 프로그램인가요?  (What is this?)
-------------

트위치TV, 아프리카TV, 카카오TV, 유튜브, 등... 요즘 인터넷 방송이 많은 분야에서 활용되고있습니다.

인터넷 방송의 길이가 짧은것은 10분 짜리부터 긴 것은 10시간이 넘어가는 방송도 있는데요 .
**(주로 대회 중계 영상이 길이가 길어요)**

이런 길이가 긴 영상의 채팅 내용을 분석해서 유의미한 결과를 분석해 내고자 할 때 쓰려고 만든 도구 입니다.

트위치 형태소 분석기를 사용하여 C#기반으로 작성하였습니다.



> **사용법:**
> **korean_chat_analyzer.exe 채팅파일.txt**
> 
> - [00:00:00.078] 닉네임: 채팅내용 으로 이루어진 텍스트문서를 입력파일로 넣어야합니다.
> - 채팅 파일은 프로젝트에 포함된 example_data.txt 를 참고하세요
>
> **usage :**
> **korean_chat_analyzer.exe chat_file.txt **
> no option


분석결과의 예시 입니다

![](/images/img1.jpg)

한글의 각 품사별 발생 빈도수 입니다

![](/images/img2.jpg)

모든 형태소 중 명사의 발생 빈도수 입니다

![](/images/img3.jpg)

모든 형태소 중 명사의 발생 빈도수 입니다
