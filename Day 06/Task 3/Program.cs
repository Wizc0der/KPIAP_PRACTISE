using static System.Console;

delegate void VolumeChangedHandler(int newVolume);

class VolumeControl
{
    public event VolumeChangedHandler VolumeChanged;
    private int volume;

    public void SetVolume(int newVolume)
    {
        volume = newVolume;
        VolumeChanged?.Invoke(volume);
    }
}

class Display
{
    public Display(VolumeControl vc)
    {
        vc.VolumeChanged += OnVolumeChanged;
    }

    void OnVolumeChanged(int volume)
    {
        WriteLine($"Громкость: {volume}%");
    }
}

class SpeakerSystem
{
    public SpeakerSystem(VolumeControl vc)
    {
        vc.VolumeChanged += OnVolumeChanged;
    }

    void OnVolumeChanged(int volume)
    {
        WriteLine($"Усилитель: {volume}%");
    }
}

class Program
{
    static void Main()
    {
        var vc = new VolumeControl();
        new Display(vc);
        new SpeakerSystem(vc);

        vc.SetVolume(30);
        vc.SetVolume(60);
        vc.SetVolume(90);
    }
}