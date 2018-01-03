# Spotify2Youtube
Connects to your spotify account via OAuth and searches youtube for all your saved spotify tracks and downloads them with appropriate ID3 tags

## Disclaimer
This is my own personal project that I started because I needed to download all my saved songs on spotify. Therefore, the code provided comes as-is and the neccessary credits go to the original developers of the different packages I am using to make my life easier.

### Requirements
You need to have ffmpeg.exe and ffprobe.exe in the same folder as the application
Also, you will need to provide your own API keys to successfully build this project, please follow the build instructions to include your API keys.

### Build Instructions
1. Open solution in Visual Studio
2. Add ffmpeg.exe and ffprobe.exe into the project and mark them as copy always and hope for the best (Am not in the publishing stage yet)
3. Create a Keys.config in the Properties folder formatted as such:

```xml
<appSettings>
  <add key="youtubeApi" value="YOUR_YOUTUBE_API_HERE" />
  <add key="spotifyApi" value="YOUR_SPOTIFY_APP_CLIENT_ID_HERE" />
</appSettings>```

4. Hope for the best, I have no clue if this will work on your machine. Contact me if you are interested
