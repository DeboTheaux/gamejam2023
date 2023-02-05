using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Affect
{
    public enum Type { SPEED };
    public enum BodyPart { LEG, ARM, HEARTH };
    public Type type;
    public float value;
}
