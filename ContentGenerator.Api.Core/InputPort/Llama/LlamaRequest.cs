namespace ContentGenerator.Api.Core.InputPort.Llama
{
    public class LlamaRequest(string? promptRequest)
    {
        public string? model { get; set; } = "mistralai/Mistral-7B-Instruct-v0.2";
        public int max_tokens { get; set; } = 512;
        public string? prompt { get; set; } = $"[INST] {promptRequest} [/INST]";
        public double temperature { get; set; } = 0.0;
        public double top_p { get; set; } = 0.7;
        public int top_k { get; set; } = 50;
        public double repetition_penalty { get; set; } = 1;
        public bool stream_tokens { get; set; } = true;
        public string[] stop { get; set; } = ["[/INST]", "</s>"];
    }
}
