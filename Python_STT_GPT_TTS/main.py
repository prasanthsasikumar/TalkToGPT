import audio_recorder
import speech_to_text
from google.cloud import speech
from speech_to_text import speech_to_text, extract_response
import openai
import keyboard
from text_to_speech import text_to_speech
from tcp import send_string_to_unity

openai.api_key = "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"
messages = [
 {"role": "system", "content" : "You’re a kind helpful assistant"}
]



while True:
    # Record a 5-second audio clip
    duration = 5  # seconds
    filename = "audio_clip.flac"
    sample_rate = audio_recorder.sd.default.samplerate
    # Set default sample rate if None
    if sample_rate is None:
        sample_rate = 44100  # Default sample rate
    audio_clip = audio_recorder.record_audio(duration)
    audio_recorder.save_audio_as_flac(audio_clip, filename, sample_rate)

    
    
    print("Transcribing audio clip...")
    audio = speech.RecognitionAudio(
        content=open("audio_clip.flac", "rb").read(),
    )
    # Transcribe the audio clip
    config = speech.RecognitionConfig(
        language_code="en",
        audio_channel_count=1
    )
    content = speech_to_text(config, audio)
    content_text = extract_response(content)
    print(content_text)
    
    #if content is none, then we need to ask the user to repeat
    if content_text is None:
        print("Please repeat")
        continue     
    
    messages.append({"role": "user", "content": content_text})

    completion = openai.ChatCompletion.create(
      model="gpt-3.5-turbo",
      messages=messages     
    )

    chat_response = completion.choices[0].message.content
    print(f'ChatGPT: {chat_response}')
    messages.append({"role": "assistant", "content": chat_response})

    send_string_to_unity("TPE_idle1")
    text_to_speech(chat_response)
    #Wait for keypress spacebar
    keyboard.wait('space')
    send_string_to_unity("TPM_talk1")



