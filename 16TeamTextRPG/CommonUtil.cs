using _16TeamTextRPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _16TeamTexTRPG.Skill;

namespace _16TeamTexTRPG
{
    public class CommonUtil
    {
        public static int CheckInput(int min, int max)
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            while (true)
            {
                Console.Write(">> ");
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input >= min && input <= max)
                        return input;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        public static void WriteLine(string value, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine();
            Console.ResetColor();
        }
        public static Skill SelectedjobSkill(Player player)
        {
            List<Skill> selectedSkill = new List<Skill>();

            switch (player.job)//직업 스킬 리스트 불러오기
            {
                case "전사":
                    selectedSkill = new WarriorSkill().GetWarriorSkills();
                    break;
                case "마법사":
                    selectedSkill = new WizardSkill().GetWizardSkills();
                    break;
                case "도적":
                    selectedSkill = new ThiefSkill().GetThiefSkills();
                    break;
                case "궁수":
                    selectedSkill = new ArcherSkill().GetArcherSkills();
                    break;
            }

            Console.WriteLine("[스킬목록]");
            for (int i = 0; i < selectedSkill.Count; i++)//선택된 스킬의 리스트만큼 선택지 생성
            {
                Console.WriteLine($"{i + 1}. {selectedSkill[i].skillName} 소모마나 : {selectedSkill[i].useMp}");
            }

            int choice = 0;
            while (true)
            {
                Console.Write("사용할 스킬의 번호를 입력 해주세요. : ");
                choice = int.Parse(Console.ReadLine() ?? "0");//유저의 입력 숫자 미입력시 결과값 0 출력

                if (choice >= 1 && choice <= selectedSkill.Count)//유저의 입력이 1 이상 선택지 이하 인지 검사 
                {
                    break;//입력한 값이 반복문 밖 다음코드에 출력
                }
                else
                {
                    Console.WriteLine("잘못 입력 하셧습니다."); //결과값 0 출력시 나오는 문구
                }
            }
            return selectedSkill[choice - 1]; //selectedSkill 변수의 값을 SelectedjobSkill 메서드로 지정
            //매개 변수값에 지정하는값은 항상 메서드첫번째{} 안에 있어야한다
        }
    }
}
