import sounddevice as sd
import soundfile as sf

def record_audio(duration):
    # Check available input devices
    devices = sd.query_devices()
    input_devices = [device for device in devices if device['max_input_channels'] > 0]

    if len(input_devices) == 0:
        print("No input devices found.")
        return None

    # Set the first available input device as the default
    default_device = input_devices[0]['name']
    sd.default.device = default_device

    print(f"Recording audio from {default_device}...")

    # Set default sample rate if None
    if sd.default.samplerate is None:
        sd.default.samplerate = 44100  # Default sample rate

    # Start recording
    audio = sd.rec(int(duration * sd.default.samplerate), channels=1)
    sd.wait()

    print("Recording complete.")

    return audio

def save_audio_as_flac(audio, filename, sample_rate):
    sf.write(filename, audio, sample_rate, format='FLAC')
    print(f"Audio clip saved to {filename}.")
