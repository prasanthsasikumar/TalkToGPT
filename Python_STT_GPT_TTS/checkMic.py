import pyaudio
import wave

def check_microphones():
    pa = pyaudio.PyAudio()
    num_devices = 1#pa.get_device_count()

    for device_index in range(num_devices):
        device_info = pa.get_device_info_by_index(device_index)
        device_name = device_info['name']
        
        print(f"Checking microphone: {device_name}")
        
        try:
            # Open a stream with the current microphone
            stream = pa.open(
                format=pyaudio.paInt16,
                channels=1,
                rate=44100,
                input=True,
                frames_per_buffer=1024,
                input_device_index=device_index
            )
            
            # Record a small sample of audio
            frames = []
            for _ in range(0, int(44100 / 1024 * 5)):  # 5 seconds of audio
                data = stream.read(1024)
                frames.append(data)
            
            # Save the recorded audio to a WAV file for inspection
            filename = f"mic_{device_index}.wav"
            wave_file = wave.open(filename, 'wb')
            wave_file.setnchannels(1)
            wave_file.setsampwidth(pa.get_sample_size(pyaudio.paInt16))
            wave_file.setframerate(44100)
            wave_file.writeframes(b''.join(frames))
            wave_file.close()
            
            print(f"Microphone {device_name} is working.")
            
        except Exception as e:
            print(f"Microphone {device_name} is not working: {str(e)}")

        stream.stop_stream()
        stream.close()

    pa.terminate()

# Run the function to check microphones
check_microphones()
