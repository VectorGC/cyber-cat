using Bonsai;
using Bonsai.Core;
using Zenject;

[BonsaiNode("HackerVision/", "Condition")]
public class IsActiveHackerVision : ConditionalAbort
{
    private HackerVision _hackerVision;

    [Inject]
    private void Construct(HackerVision hackerVision)
    {
        _hackerVision = hackerVision;
    }

    public override bool Condition()
    {
        return _hackerVision.Active;
    }
}