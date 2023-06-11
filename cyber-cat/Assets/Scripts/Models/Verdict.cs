namespace Models
{
    public class Verdict : IVerdict
    {
        private readonly VerdictStatus _status;
        private readonly string _error;
        private readonly int _testPassed;

        public Verdict(VerdictStatus status, string error, int testPassed)
        {
            _status = status;
            _error = error;
            _testPassed = testPassed;
        }

        public override string ToString()
        {
            return $"{nameof(_status)}: {_status}, {nameof(_error)}: {_error}, {nameof(_testPassed)}: {_testPassed}";
        }
    }
}