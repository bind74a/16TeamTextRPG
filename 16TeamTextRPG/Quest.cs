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
        }

        public void ShowMain()
        {
            Console.WriteLine("Quest!!\n");

            int idx = 0;
            foreach (Quest quest in listQuest)
            {
                idx++;
                Console.WriteLine($"idx. {quest.title}");
            }
            Console.WriteLine();

            Console.WriteLine("원하시는 퀘스트를 선택해주세요.");
            int input = 0;

            ShowQuest(input);
        }

        public void ShowQuest(int questIdx)
        {
            Console.WriteLine("Quest!!\n");

            // 퀘스트 제목
            Console.WriteLine(listQuest[questIdx].title);
            Console.WriteLine();

            // 퀘스트 설명
            Console.WriteLine(listQuest[questIdx].description);
            Console.WriteLine();

            // 퀘스트 목표

            // 보상
            Console.WriteLine("[보상]\n");
            Console.WriteLine("Quest!!\n");
            Console.WriteLine("Quest!!\n");


        }
    }

    class Quest
    {
        public string title; // 제목
        public string description; // 설명
        public int goal; // 퀘스트 목표
        public int progress; // 퀘스트 진행사항

        // 보상
        public int gold;
        public Item item;
    }
}
