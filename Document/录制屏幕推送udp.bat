f:
cd F:\Solution\HeBianGu.Product.FFmpeg.MediaConverter\HeBianGu.Product.FFmpeg.MediaConverter
ffmpeg -f gdigrab -framerate 25 -offset_x 10 -offset_y 20 -video_size 640x480 -i desktop -vcodec libx264 -preset:v ultrafast -tune:v zerolatency -f h264 udp://192.168.2.1:6666
pause