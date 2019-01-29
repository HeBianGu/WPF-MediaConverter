f:
cd F:\Solution\HeBianGu.Product.FFmpeg.MediaConverter\HeBianGu.Product.FFmpeg.MediaConverter
ffmpeg -re -i eguid.flv -vcodec copy -acodec copy -f flv -y rtmp://eguid.cc:1935/rtmp/eguid
pause