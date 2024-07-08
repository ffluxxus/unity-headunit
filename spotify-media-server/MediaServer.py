import spotipy
import spotipy.util as util
from spotipy.oauth2 import SpotifyOAuth, SpotifyClientCredentials
import requests
import socket
import json
import time
import os

# THIS REQUIRES A ACTIVE INTERNET CONNECTION

# THIS REQUIRES A ACTIVE INTERNET CONNECTION

# THIS REQUIRES A ACTIVE INTERNET CONNECTION


scope = "user-read-currently-playing"
username = "username"
client_id = "clientid"
client_secret = "clientsecret"
redirect_uri = "http://127.0.0.1:8080"
token = util.prompt_for_user_token(username, scope, client_id, client_secret, redirect_uri)
sp = spotipy.Spotify(auth=token)
currentsong = sp.currently_playing()


HOST = "127.0.0.1"
PORT = 5000


def get_song_info():
    sp = spotipy.Spotify(auth=token)
    currentsong = sp.currently_playing()
    # print(currentsong)

    image = currentsong['item']['album']['images'][0]['url']
    title = currentsong['item']['name']
    artist = currentsong['item']['artists'][0]['name']
    progress = currentsong['progress_ms']
    duration = currentsong['item']['duration_ms']
    playing = currentsong['is_playing']

    song_data = [{
        "image": image,
        "title": title,
        "artist": artist,
        "start": progress,
        "end": duration,
        "playing": playing
    }]

    print(f"Image URL: {image}")
    print(f"Song Title: {title}")
    print(f"Artist Name: {artist}\n\n")

    return song_data


def main():
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    server_socket.bind((HOST, PORT))
    server_socket.listen()

    print(f"Server listening on {HOST}:{PORT}\n")

    while True:
        start_time = time.time()  # Start time measurement

        client_socket, address = server_socket.accept()
        os.system(f"title Connected to client at {address}")
        print(f"Connected by {address}")

        try:
            while True:
                song_data = get_song_info()
                data_json = json.dumps(song_data)
                client_socket.sendall(data_json.encode())
                time.sleep(0.7) # decrease by an amount to make it exact second loop dir

                # Calculate and print loop execution time
                loop_time = (time.time() - start_time) * 1000  # milliseconds
                print(f"Loop Execution Time: {loop_time:.2f} ms")
                start_time = time.time()  # Reset timer for next loop

        except ConnectionError as e:
            os.system(f"title Client disconnected")
            print(f"Client disconnected: {e}\n")
        finally:
            client_socket.close()


if __name__ == "__main__":
    os.system("title Waiting for client...")
    main()
