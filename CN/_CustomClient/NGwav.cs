
namespace WiseM.Client
{
    class NGwav
    {
        public void NGStart()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = System.IO.Directory.GetCurrentDirectory()+@"\NG.wav";
            player.Load();
            player.Play();
            
        }
    }
}
