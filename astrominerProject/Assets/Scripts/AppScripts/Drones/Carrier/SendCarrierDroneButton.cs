namespace SBaier.Astrominer
{
    public class SendCarrierDroneButton : SendDroneButton<CarrierDrone>
    {
	    protected override bool GetButtonActive()
	    {
		    return base.GetButtonActive() &&
		           _target.HasOwningPlayer &&
		           _target.OwningPlayer == _player;
	    }
    }
}
