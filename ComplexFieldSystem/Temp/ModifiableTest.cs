using MSFD;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ModifiableTest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Delta deltaFloat = new Delta(1.5f);
    [SerializeField]
    DeltaInt deltaInt = new DeltaInt(3);

    [SerializeField]
    ModProcessor<int> cascadeModifierInt = new ModProcessor<int>();

    [SerializeField]
    ModField<float> modifiableFieldFloat = new ModField<float>();

    [SerializeField]
    DeltaE deltaFloatE = new DeltaE(1.5f);

    [SerializeField]
    DeltaIntE deltaIntE = new DeltaIntE(133);
    private void Start()
    {
        deltaFloat.AddMod((x) => x + 3);
        deltaFloatE.AddMod((x) => x - 3);
        deltaFloatE.AddChangeMod((x) => x * 2);

        modifiableFieldFloat.AddMod(TestModifier);
    }
    float TestModifier(float x)
    {
        return x + 300;
    }
}


