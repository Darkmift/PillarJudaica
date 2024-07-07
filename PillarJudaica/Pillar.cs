namespace PillarJudaica
{
    enum JudgeEnum
    {
        Rashi,
        Tur
    }
    enum Rashut
    {
        Public,
        Carmelit,
        Single,
        Patur
    }
    internal class Pillar
    {
        private int TSize { get; set; }
        private int Twidth { get; set; }
        private Rashut ParentRashut { get; set; }
        private JudgeEnum Judge { get; set; }
        private string ConsideredRashut
        {
            get => $"{GetStatus()}";
        }

        public Pillar(Rashut parentRashut, int tSize, int tWidth, JudgeEnum judge)
        {
            TSize = TSize;
            ParentRashut = parentRashut;
            Twidth = tWidth;
            Judge = judge;
        }

        public Rashut GetStatus() => ParentRashut switch
        {
            Rashut.Public => GetPublicStatus(),
            Rashut.Carmelit => GetCarmelitStatus(),
            _ => Rashut.Single,
        };

        private Rashut GetPublicStatus() => TSize switch
        {
            _ when TSize < 3 || TSize > 9 => Rashut.Public,
            _ when Twidth > 4 && TSize > 3 && TSize < 9 => Rashut.Carmelit,
            _ when Twidth < 4 && (TSize > 10 || TSize < 9) => Rashut.Patur,
            _ => Rashut.Single
        };

        private Rashut GetCarmelitStatus() => TSize switch
        {
            _ when TSize < 9 => Rashut.Carmelit,
            _ when TSize >= 10 && Twidth < 4 => Rashut.Patur,
            _ when TSize >= 10 => Rashut.Single,
            _ => Rashut.Carmelit
        };


        public Rashut GetStatusLegacy()
        {
     
            if (ParentRashut == Rashut.Public)
            {
                Rashut status = TSize switch
                {
                    _ when TSize < 3 && TSize > 9 => Rashut.Public,
                    _ when Twidth > 4 && TSize > 3 && TSize < 9 => Rashut.Carmelit,
                    _ when Twidth < 4 && TSize > 10 || TSize < 9 => Rashut.Patur,
                    _ => Rashut.Single
                };
                return status;
            }
            
            if (ParentRashut == Rashut.Carmelit)
            {
                Rashut status = TSize switch
                {
                    _ when TSize < 10 && Twidth >= 4 => Rashut.Carmelit,

                    _ when TSize > 10 && Twidth >= 4 => Rashut.Single,
                    _ when TSize > 10 && Twidth <= 4 => Rashut.Single,
                    _ when Twidth < 4 && TSize > 10 || TSize < 9 => Judge == JudgeEnum.Rashi ? Rashut.Patur : Rashut.Carmelit,
                    _ => Rashut.Single
                };
                return status;
            }

            return Rashut.Single;
        }
    }
}
