f:
cd F:\Solution\HeBianGu.Product.FFmpeg.MediaConverter\HeBianGu.Product.FFmpeg.MediaConverter
ffmpeg -f gdigrab -framerate 25 -offset_x 10 -offset_y 20 -video_size 640x480 -i desktop out.mpg 
pause