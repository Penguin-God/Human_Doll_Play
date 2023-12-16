using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnvirmentTestHelper
{
    public static TestEnvirment CreateTestEnvirment() => new TestEnvirment();
    public class TestEnvirment : IEnvirment
    {
        public bool Flag;
        public void ChangeEnvierment(int value) => Flag = value == 1;
    }
}
