namespace Wordle.Models
{
    public class Word
    {
        public string word;
        public int size;
        private readonly HttpClient client;

        public Word()
        {
            client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            GetWordAsync().Wait();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<String> GetWordAsync()
        {
            word = await client.GetStringAsync("https://random-word-api.herokuapp.com/word");
            word = word.Split('"')[1];
            size = word.Length;

            return word;
        }
    }
}
