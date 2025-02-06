using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _16TeamTextRPG
    {
        internal class Stage
        {
            //구현할것 : 전투신 전체의 로직
            static List<Monster> monsters = new List<Monster>();

            static Random Random = new Random();


            public List<Monster> SummonMonster()//몬스터 소환
            {
                //1.몬스터 클래스에서 몬스터 정보받고 그정보로 몬스터 랜덤성으로 1~4마리 소환
                List<Monster> list = new List<Monster>();

                int monsterObj = Random.Next(1, 5); //몬스터 마리수 결정

                for (int i = 0; i < monsterObj; i++)
                {
                    int randomMon = Random.Next(monsters.Count);//리스트 안 객체를 랜덤하게 정하는것
                    Console.WriteLine($"몬스터 소환 {monsters[randomMon]}");
                    list.Add(monsters[randomMon]);
                }
                return list;
            }

            public void Battlefield()
            {

                //소환됀 객체들을 필드위에 뛰운다 그리고 필드 몬스터 리스트화?
                Console.WriteLine("Battle!!");
                Console.WriteLine();
                List<Monster> list = SummonMonster();

                Console.WriteLine();
                Console.WriteLine("[내정보]");
            }
            //2.공격하기 선택창을 추가하여 공격시 전투 계산 메소드 작동
            //3.전투가 끝날시 남아있는 현재체력과 현재 스탯표시
        }
    }

