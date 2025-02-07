using _16TeamTextRPG;
using Class_shop_DS_ver._6._0;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _16TeamTextRPG
{
    class Guild
    {
        List<Quest> listQuest;

        Player player;

        public Guild(Player _player)
        {
            listQuest = new List<Quest>();
            player = _player;

            QuestInfo info = new QuestInfo();
            info.title          = "마을을 위협하는 미니언 처치";
            info.description    = "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\n" +
                                  "마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n" +
                                  "모험가인 자네가 좀 처치해주게!";
            info.goalDesc       = "미니언 5마리 처치";
            info.goal = 5;
            info.gold = 100;
            //info.item = 아이템
            listQuest.Add(new Quest(info));


        }

        public void ShowMain()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Quest!!\n");

                // 퀘스트 목록
                int idx = 0;
                string progress = "";
                foreach (Quest quest in listQuest)
                {
                    idx++;
                    if (quest.clear) { progress = "퀘스트 완료"; }
                    else if (quest.accept) { progress = "퀘스트 진행 중"; }

                    Console.WriteLine($"{idx}. {quest.title} ({progress})");
                }
                Console.WriteLine("0. 나가기\n");

                int input = Monster.CheckInput(0, listQuest.Count + 1);

                // 나가기
                if (input == 0)
                    return;

                ShowQuest(input);
            }
        }

        public void ShowQuest(int questIdx)
        {
            Quest quest = listQuest[questIdx];

            // 이미 클리어한 퀘스트라면 main으로 돌아가기
            if (quest.clear)
                return;

            Console.WriteLine("Quest!!\n");

            // 퀘스트 제목
            Console.WriteLine(quest.title);
            Console.WriteLine();

            // 퀘스트 설명
            Console.WriteLine(quest.description);
            Console.WriteLine();

            // 퀘스트 목표 - 미니언 5마리 처치 (0/5)
            Console.WriteLine($"{quest.goalDesc} ({quest.progress}/{quest.goal})");
            Console.WriteLine();

            // 보상
            Console.WriteLine("[보상]\n");
            Console.WriteLine($"{quest.item.Name}\n");
            Console.WriteLine($"{quest.gold}G\n");

            // 수락, 보상받기
            if (quest.accept) 
            {
                Console.WriteLine("1. 보상 받기\n");
                Console.WriteLine("2. 돌아가기\n");
            }
            else
            {
                Console.WriteLine("1. 수락\n");
                Console.WriteLine("2. 거절\n");
            }

            while (true)
            {
                int input = Monster.CheckInput(1, 2);
                // main으로 돌아가기
                if (input == 2)
                    break;

                // 퀘스트를 받은 상태라면
                if (quest.accept)
                {
                    if (GetReward(quest))
                        break;
                }
                // 퀘스트 수락
                else
                {
                    quest.accept = true;
                    break;
                }
            }
        }

        public void UpdateProgress(int questIdx)
        {
            listQuest[questIdx].progress++;
        }

        private bool GetReward(Quest quest)
        {
            // 퀘스트 목표보다 진행상황이 높으면 보상 지급
            if (quest.goal <= quest.progress)
            {
                quest.clear = true; // 퀘스트 완료

                // 보상 지급
                player.Inven.Add(quest.item);
                player.Gold += quest.gold;
                
                Console.WriteLine("보상 지급완료!");
                Thread.Sleep(500);

                return true;
            }
            else
            {
                Console.WriteLine("퀘스트를 완료하지 못했습니다.");
                return false;
            }
        }
    }

    struct QuestInfo
    {
        public string title; // 제목
        public string description; // 설명
        public string goalDesc; // 목표 설명

        public int goal; // 목표
        public int progress; // 진행상황

        public bool accept; // 퀘스트 수락여부
        public bool clear; // 클리어

        // 보상
        public int gold;
        public Item item;
    }

    class Quest
    {
        public string title; // 제목
        public string description; // 설명
        public string goalDesc; // 목표 설명

        public int goal; // 목표
        public int progress; // 진행사항

        public bool accept; // 퀘스트 수락여부
        public bool clear; // 클리어

        // 보상
        public int gold;
        public Item item;

        public Quest(QuestInfo info)
        {
            title = info.title;
            description = info.description;
            goalDesc = info.goalDesc;

            goal = info.goal;
            progress = 0;

            accept = false;
            clear = false;

            gold = info.gold;
            item = info.item;
        }
    }
}
