namespace Test.Utilities {
    class TestUtilities {
        public string GenerateStringOfSize(int size) {
            Random random = new Random();
            char[] chars = new char[size];
            for (int i = 0; i < size; i++) {
                chars[i] = (char)random.Next('a', 'z' + 1); 
            }
            return new string(chars);
        }

        public bool AreByteArraysEqual(byte[] arr1, byte[] arr2) {
            if (arr1.Length != arr2.Length)
                return false;

            for (int i = 0; i < arr1.Length; i++) {
                if (arr1[i] != arr2[i])
                    return false;
            }

            return true;
        }
    }
}