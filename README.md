# TalkToGPT

TalkToGPT is a project that brings a Virtual Chat GPT assistant to life, allowing users to interact with it by simply pressing a physical button. This assistant listens to the user, responds back, and aims to create a conversational experience.

![combined](https://github.com/prasanthsasikumar/TalkToGPT/blob/main/Images/combined.png?raw=true)


Here is a working video: https://youtube.com/shorts/8U4MYI8BbWE?feature=share

## Features

- Interactive virtual assistant: Users can have natural language conversations with the Virtual Chat GPT assistant.
- Speech-to-Text (STT): Utilizes Google Speech-to-Text to convert user speech into text.
- Text-to-Speech (TTS): Employs Google Text-to-Speech to generate spoken responses from the assistant.
- Unity display animation: The virtual assistant's display animation is created using the Unity game engine.
- Python integration: The project is implemented primarily in Python, with the Unity game engine and Python communicating through TCP IP.

## Project Structure

The project is organized into the following structure:

- Python_STT_GPT_TTS/
  - chatgpt.py
  - speech_to_text.py
  - text_to_speech.py
  - ...
- Unity_Display_Animation/
  - ...
  - ...
- ...


The `Python_STT_GPT_TTS` folder contains the Python code responsible for handling the speech-to-text, text-to-speech, and chat GPT functionalities. It includes modules such as `chatgpt.py`, `speech_to_text.py`, and `text_to_speech.py`, along with other relevant files.

The `Unity_Display_Animation` folder houses the Unity project responsible for creating the virtual assistant's display animation. It includes the necessary assets, scripts, and other files required for the Unity game engine.

## Getting Started

To get started with TalkToGPT, follow the steps below:

1. Clone the repository:

   ```bash
   git clone https://github.com/your-username/TalkToGPT.git
   ```
2. Set up the Unity Display Animation:

Open the Unity project located in the Unity_Display_Animation folder using Unity game engine.
Customize and configure the virtual assistant's display animation to suit your preferences.
3. Install dependencies:

Ensure you have Python installed on your system.

Install the required Python packages by running the following command:
  ```bash
  pip install -r requirements.txt
  ```
4. Configure Google API credentials:

Obtain API credentials for both Google Speech-to-Text and Google Text-to-Speech.
Add the credentials to the appropriate Python files (speech_to_text.py and text_to_speech.py) as instructed in their respective files.
5. Run the project:

Start the Unity display animation by running it from the Unity game engine.
Run the Python code responsible for speech-to-text, text-to-speech, and chat GPT functionalities.

## Contribution
Contributions to the TalkToGPT project are welcome! If you'd like to contribute, please follow these steps:

Fork the repository.
Create a new branch for your feature or bug fix.
Make your changes and commit them, providing descriptive commit messages.
Push your changes to your fork.
Submit a pull request detailing your changes and their benefits.

## License
The TalkToGPT project is licensed under the MIT License. Feel free to use, modify, and distribute the code for your own purposes.
