using System;

namespace FinalTask.Models
{
    public class PlayerProfile
    {
        public string Name { get; set; }
        public int Balance { get; set; }
        public bool IsActive { get; set; }

        public PlayerProfile()
        {
            Name = "Unknown Player";
            Balance = 1000;
            IsActive = true;
        }

        public PlayerProfile(string name) : this()
        {
            Name = name;
        }

        public bool CanPlaceBet(int betAmount)
        {
            return betAmount > 0 && betAmount <= Balance;
        }

        public void ProcessWin(int winAmount)
        {
            if (winAmount < 0)
                throw new ArgumentException("Сумма выигрыша не может быть отрицательной");

            Balance += winAmount;
        }

        public void ProcessLoss(int lossAmount)
        {
            if (lossAmount < 0)
                throw new ArgumentException("Сумма проигрыша не может быть отрицательной");

            Balance = Math.Max(0, Balance - lossAmount);
        }

        public bool IsBroke()
        {
            return Balance <= 0;
        }

        public void ResetProfile()
        {
            Balance = 1000;
            IsActive = true;
        }

        public override string ToString()
        {
            string status = IsActive ? "Активен" : "Заблокирован";
            return $"{Name}: {Balance} монет ({status})";
        }
    }
}