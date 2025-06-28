using Polly;

namespace AccountService.Infrastructure.Helpers
{
    public static class TransientHttpErrorPolicyFactory
    {
        /// <summary>
        /// The number of retries before it gives up on the HTTP call.
        /// </summary>
        private const int _numOfRetries = 3;

        /// <summary>
        /// The wait time between retries
        /// </summary>
        private const int _waitTimeInMilliseconds = 600;

        /// <summary>
        /// Adds a Policy to wait (600ms) and retry 3 times on Transient HTTP Errors
        /// </summary>
        /// <param name="policyBuilder">The Policy Builder for an HttpResponseMessage</param>
        public static IAsyncPolicy<HttpResponseMessage> CreateTransientHttpErrorPolicy(PolicyBuilder<HttpResponseMessage> policyBuilder) =>
            policyBuilder.WaitAndRetryAsync(_numOfRetries, _ => TimeSpan.FromMilliseconds(_waitTimeInMilliseconds));
    }
}
