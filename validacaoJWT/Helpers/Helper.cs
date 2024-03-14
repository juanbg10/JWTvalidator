namespace validacaoJWT.Helpers
{
    public static class Helper
    {
        public static bool IsPrimeNumber(int number)
        {
            if (number <= 1) return false;
            if (number <= 3) return true;
            if (number % 2 == 0 || number % 3 == 0) return false;

            for (int i = 5; i * i <= number; i += 6)
            {
                if (number % i == 0 || number % (i + 2) == 0)
                    return false;
            }

            return true;
        }

        public static int GeneratePrimeNumbers()
        {
            Random random = new Random();
            int num = random.Next(100, 1000); // Geração de um número aleatório entre 100 e 1000

            while (!PrimeNumberValidator(num))
            {
                num = random.Next(100, 1000);
            }

            return num;
        }

        public static bool PrimeNumberValidator(int number)
        {
            if (number <= 1)
            { return false; }

            if (number <= 3)
            { return true; }

            if (number % 2 == 0 || number % 3 == 0)
            { return false; }

            for (int i = 5; i * i <= number; i += 6)
            {
                if (number % i == 0 || number % (i + 2) == 0)
                { return false; }
            }

            return true;
        }
    }
}
