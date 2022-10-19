namespace YuGiHoTcgSdk.Infrastructure.HttpClients.Common
{
    internal static class Global
    {
        internal const string CardApiBase = "cardinfo.php";
        internal const string SetInfoApiBase = "cardsetsinfo.php";
        internal const string SetsApiBase = "cardsets.php";
        internal const string ArchetypeApiBase = "archetypes.php";

        internal static class Monster
        {
            internal const string All = $"{Normal},{NormalTuner}," +
                                        $"{Effect}," +
                                        $"{Tuner}," +
                                        $"{Flip},{FlipEffect},{FlipTunerEffect}," +
                                        $"{Spirit}," +
                                        $"{UnionEffect}," +
                                        $"{Gemini}," +
                                        $"{PendulumEffect},{PendulumNormal},{PendulumTunerEffect}," +
                                        $"{Ritual},{RitualEffect}," +
                                        $"{Toon}," +
                                        $"{Fusion}," +
                                        $"{Synchro},{SynchroTuner},{SynchroPendulumEffect}" +
                                        $"{XYZ},{XYZPendulumEffect},{Link}," +
                                        $"{PendulumFlipEffect},{PendulumEffectFusion}";
            internal const string Normal = "Normal Monster";
            internal const string NormalTuner = "Normal Tuner Monster";
            internal const string Effect = "Effect Monster";
            internal const string Tuner = "Tuner Monster";
            internal const string Flip = "Flip Monster";
            internal const string FlipEffect = "Flip Effect Monster";
            internal const string FlipTunerEffect = "Flip Tuner Effect Monster";
            internal const string Spirit = "Spirit Monster";
            internal const string UnionEffect = "Union Effect Monster";
            internal const string Gemini = "Gemini Monster";
            internal const string PendulumEffect = "Pendulum Effect Monster";
            internal const string PendulumNormal = "Pendulum Normal Monster";
            internal const string PendulumTunerEffect = "Pendulum Tuner Effect Monster";
            internal const string Ritual = "Ritual Monster";
            internal const string RitualEffect = "Ritual Effect Monster";
            internal const string Toon = "Toon Monster";
            internal const string Fusion = "Fusion Monster";
            internal const string Synchro = "Synchro Monster";
            internal const string SynchroTuner = "Synchro Tuner Monster";
            internal const string SynchroPendulumEffect = "Synchro Pendulum Effect Monster";
            internal const string XYZ = "XYZ Monster";
            internal const string XYZPendulumEffect = "XYZ Pendulum Effect Monster";
            internal const string Link = "Link Monster";
            internal const string PendulumFlipEffect = "Pendulum Flip Effect Monster";
            internal const string PendulumEffectFusion = "Pendulum Effect Fusion Monster";

        }
    }
}