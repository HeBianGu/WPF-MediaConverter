f:
cd F:\Solution\HeBianGu.Product.FFmpeg.MediaConverter\HeBianGu.Product.FFmpeg.MediaConverter
@echo ffmpeg -i out(2).mp4 -vcodec 3g2 out.3gp
@echo ffmpeg -i out(2).mp4 -y -b 20 -s sqcif -r 10 -acodec amr_wb -ab 23.85 -ac 1 -ar 16000 test.3gp
ffmpeg -i out(2).mp4 -vcodec wmv1 out22222.wmv
pause