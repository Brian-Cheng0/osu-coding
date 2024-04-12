from huggingface_hub import hf_hub_download
hf_hub_download(repo_id="pszemraj/BERTopic-summcomparer-gauntlet-v0p1-all-roberta-large-v1-document_text", filename="config.json")

# hf_hub_download(repo_id="google/fleurs", filename="fleurs.py", repo_type="dataset")



from bertopic import BERTopic
topic_model = BERTopic.load("pszemraj/BERTopic-summcomparer-gauntlet-v0p1-all-roberta-large-v1-document_text")

topic_model.get_topic_info()
