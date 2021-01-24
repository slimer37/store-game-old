using UnityEngine;
using UnityEngine.InputSystem;

public class Extinguisher : Tool
{
    [SerializeField] private float reach;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private Animator anim;
    [SerializeField] private string useState;
    [SerializeField] private string useStopState;

    protected override void OnUse(InputAction.CallbackContext ctx) => Spray(FireStarter.Extinguishing = ctx.ReadValue<float>() == 1);

    void Spray(bool value)
    {
        if (value)
        {
            particles.Play();
            anim.Play(useState);
        }
        else
        {
            particles.Stop();
            anim.Play(useStopState);
        }
    }

    protected override void Pickup(bool pickup)
    {
        base.Pickup(pickup);
        Spray(false);
    }

    void Update()
    {
        Ray ray = Interaction.Current.Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, reach))
        {

        }
    }
}
