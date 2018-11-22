using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HeBianGu.Product.FFmpeg.Driver;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest.Driver
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string ss = @"File formats:
 D. = Demuxing supported
 .E = Muxing supported
 --
  E 3g2             3GP2 (3GPP2 file format)
  E 3gp             3GP (3GPP file format)
 D  4xm             4X Technologies
  E a64             a64 - video for Commodore 64
 D  aac             raw ADTS AAC (Advanced Audio Coding)
 DE ac3             raw AC-3
 D  act             ACT Voice file format
 D  adf             Artworx Data Format
 D  adp             ADP
  E adts            ADTS AAC (Advanced Audio Coding)
 DE adx             CRI ADX
 D  aea             MD STUDIO audio
 D  afc             AFC
 DE aiff            Audio IFF
 DE alaw            PCM A-law
 D  alias_pix       Alias/Wavefront PIX image
 DE amr             3GPP AMR
 D  anm             Deluxe Paint Animation
 D  apc             CRYO APC
 D  ape             Monkey's Audio
 D  apng            Animated Portable Network Graphics
 D  aqtitle         AQTitle subtitles
 DE asf             ASF (Advanced / Active Streaming Format)
  E asf_stream      ASF (Advanced / Active Streaming Format)
 DE ass             SSA (SubStation Alpha) subtitle
 DE ast             AST (Audio Stream)
 DE au              Sun AU
 DE avi             AVI (Audio Video Interleaved)
 D  avisynth        AviSynth script
  E avm2            SWF (ShockWave Flash) (AVM2)
 D  avr             AVR (Audio Visual Research)
 D  avs             AVS
 D  bethsoftvid     Bethesda Softworks VID
 D  bfi             Brute Force & Ignorance
 D  bin             Binary text
 D  bink            Bink
 DE bit             G.729 BIT file format
 D  bmp_pipe        piped bmp sequence
 D  bmv             Discworld II BMV
 D  boa             Black Ops Audio
 D  brender_pix     BRender PIX image
 D  brstm           BRSTM (Binary Revolution Stream)
 D  c93             Interplay C93
  E caca            caca (color ASCII art) output device
 DE caf             Apple CAF (Core Audio Format)
 DE cavsvideo       raw Chinese AVS (Audio Video Standard) video
 D  cdg             CD Graphics
 D  cdxl            Commodore CDXL video
 D  cine            Phantom Cine
 D  concat          Virtual concatenation script
  E crc             CRC testing
  E dash            DASH Muxer
 DE data            raw data
 DE daud            D-Cinema audio
 D  dfa             Chronomaster DFA
 DE dirac           raw Dirac
 DE dnxhd           raw DNxHD (SMPTE VC-3)
 D  dpx_pipe        piped dpx sequence
 D  dsf             DSD Stream File (DSF)
 D  dshow           DirectShow capture
 D  dsicin          Delphine Software International CIN
 DE dts             raw DTS
 D  dtshd           raw DTS-HD
 DE dv              DV (Digital Video)
  E dvd             MPEG-2 PS (DVD VOB)
 D  dxa             DXA
 D  ea              Electronic Arts Multimedia
 D  ea_cdata        Electronic Arts cdata
 DE eac3            raw E-AC-3
 D  epaf            Ensoniq Paris Audio File
 D  exr_pipe        piped exr sequence
 DE f32be           PCM 32-bit floating-point big-endian
 DE f32le           PCM 32-bit floating-point little-endian
  E f4v             F4V Adobe Flash Video
 DE f64be           PCM 64-bit floating-point big-endian
 DE f64le           PCM 64-bit floating-point little-endian
 DE ffm             FFM (FFserver live feed)
 DE ffmetadata      FFmpeg metadata in text
 D  film_cpk        Sega FILM / CPK
 DE filmstrip       Adobe Filmstrip
 DE flac            raw FLAC
 D  flic            FLI/FLC/FLX animation
 DE flv             FLV (Flash Video)
  E framecrc        framecrc testing
  E framemd5        Per-frame MD5 testing
 D  frm             Megalux Frame
 DE g722            raw G.722
 DE g723_1          raw G.723.1
 D  g729            G.729 raw format demuxer
 D  gdigrab         GDI API Windows frame grabber
 DE gif             GIF Animation
 D  gsm             raw GSM
 DE gxf             GXF (General eXchange Format)
 DE h261            raw H.261
 DE h263            raw H.263
 DE h264            raw H.264 video
  E hds             HDS Muxer
 DE hevc            raw HEVC video
  E hls             Apple HTTP Live Streaming
 D  hls,applehttp   Apple HTTP Live Streaming
 D  hnm             Cryo HNM v4
 DE ico             Microsoft Windows ICO
 D  idcin           id Cinematic
 D  idf             iCE Draw File
 D  iff             IFF (Interchange File Format)
 DE ilbc            iLBC storage
 DE image2          image2 sequence
 DE image2pipe      piped image2 sequence
 D  ingenient       raw Ingenient MJPEG
 D  ipmovie         Interplay MVE
  E ipod            iPod H.264 MP4 (MPEG-4 Part 14)
 DE ircam           Berkeley/IRCAM/CARL Sound Format
  E ismv            ISMV/ISMA (Smooth Streaming)
 D  iss             Funcom ISS
 D  iv8             IndigoVision 8000 video
 DE ivf             On2 IVF
 D  j2k_pipe        piped j2k sequence
 DE jacosub         JACOsub subtitle format
 D  jpeg_pipe       piped jpeg sequence
 D  jpegls_pipe     piped jpegls sequence
 D  jv              Bitmap Brothers JV
 D  kux             KUX (KUX Video)
 DE latm            LOAS/LATM
 D  lavfi           Libavfilter virtual input device
 D  live_flv        live RTMP FLV (Flash Video)
 D  lmlm4           raw lmlm4
 D  loas            LOAS AudioSyncStream
 DE lrc             LRC lyrics
 D  lvf             LVF
 D  lxf             VR native stream (LXF)
 DE m4v             raw MPEG-4 video
  E matroska        Matroska
 D  matroska,webm   Matroska / WebM
  E md5             MD5 testing
 D  mgsts           Metal Gear Solid: The Twin Snakes
 DE microdvd        MicroDVD subtitle format
 DE mjpeg           raw MJPEG video
  E mkvtimestamp_v2 extract pts as timecode v2 format, as defined by mkvtoolnix
 DE mlp             raw MLP
 D  mlv             Magic Lantern Video (MLV)
 D  mm              American Laser Games MM
 DE mmf             Yamaha SMAF
  E mov             QuickTime / MOV
 D  mov,mp4,m4a,3gp,3g2,mj2 QuickTime / MOV
  E mp2             MP2 (MPEG audio layer 2)
 DE mp3             MP3 (MPEG audio layer 3)
  E mp4             MP4 (MPEG-4 Part 14)
 D  mpc             Musepack
 D  mpc8            Musepack SV8
 DE mpeg            MPEG-1 Systems / MPEG program stream
  E mpeg1video      raw MPEG-1 video
  E mpeg2video      raw MPEG-2 video
 DE mpegts          MPEG-TS (MPEG-2 Transport Stream)
 D  mpegtsraw       raw MPEG-TS (MPEG-2 Transport Stream)
 D  mpegvideo       raw MPEG video
  E mpjpeg          MIME multipart JPEG
 D  mpl2            MPL2 subtitles
 D  mpsub           MPlayer subtitles
 D  msnwctcp        MSN TCP Webcam stream
 D  mtv             MTV
 DE mulaw           PCM mu-law
 D  mv              Silicon Graphics Movie
 D  mvi             Motion Pixels MVI
 DE mxf             MXF (Material eXchange Format)
  E mxf_d10         MXF (Material eXchange Format) D-10 Mapping
 D  mxg             MxPEG clip
 D  nc              NC camera feed
 D  nistsphere      NIST SPeech HEader REsources
 D  nsv             Nullsoft Streaming Video
  E null            raw null video
 DE nut             NUT
 D  nuv             NuppelVideo
  E oga             Ogg Audio
 DE ogg             Ogg
 DE oma             Sony OpenMG audio
  E opus            Ogg Opus
 D  paf             Amazing Studio Packed Animation File
 D  pictor_pipe     piped pictor sequence
 D  pjs             PJS (Phoenix Japanimation Society) subtitles
 D  pmp             Playstation Portable PMP
 D  png_pipe        piped png sequence
  E psp             PSP MP4 (MPEG-4 Part 14)
 D  psxstr          Sony Playstation STR
 D  pva             TechnoTrend PVA
 D  pvf             PVF (Portable Voice Format)
 D  qcp             QCP
 D  r3d             REDCODE R3D
 DE rawvideo        raw video
 D  realtext        RealText subtitle format
 D  redspark        RedSpark
 D  rl2             RL2
 DE rm              RealMedia
 DE roq             raw id RoQ
 D  rpl             RPL / ARMovie
 D  rsd             GameCube RSD
 DE rso             Lego Mindstorms RSO
 DE rtp             RTP output
 DE rtsp            RTSP output
 DE s16be           PCM signed 16-bit big-endian
 DE s16le           PCM signed 16-bit little-endian
 DE s24be           PCM signed 24-bit big-endian
 DE s24le           PCM signed 24-bit little-endian
 DE s32be           PCM signed 32-bit big-endian
 DE s32le           PCM signed 32-bit little-endian
 DE s8              PCM signed 8-bit
 D  sami            SAMI subtitle format
 DE sap             SAP output
 D  sbg             SBaGen binaural beats script
 D  sdp             SDP
 D  sdr2            SDR2
  E segment         segment
 D  sgi_pipe        piped sgi sequence
 D  shn             raw Shorten
 D  siff            Beam Software SIFF
 D  sln             Asterisk raw pcm
 DE smjpeg          Loki SDL MJPEG
 D  smk             Smacker
  E smoothstreaming Smooth Streaming Muxer
 D  smush           LucasArts Smush
 D  sol             Sierra SOL
 DE sox             SoX native
 DE spdif           IEC 61937 (used on S/PDIF - IEC958)
  E spx             Ogg Speex
 DE srt             SubRip subtitle
 D  stl             Spruce subtitle format
  E stream_segment,ssegment streaming segment muxer
 D  subviewer       SubViewer subtitle format
 D  subviewer1      SubViewer v1 subtitle format
 D  sunrast_pipe    piped sunrast sequence
 D  sup             raw HDMV Presentation Graphic Stream subtitles
  E svcd            MPEG-2 PS (SVCD)
 DE swf             SWF (ShockWave Flash)
 D  tak             raw TAK
 D  tedcaptions     TED Talks captions
  E tee             Multiple muxer tee
 D  thp             THP
 D  tiertexseq      Tiertex Limited SEQ
 D  tiff_pipe       piped tiff sequence
 D  tmv             8088flex TMV
 DE truehd          raw TrueHD
 D  tta             TTA (True Audio)
 D  tty             Tele-typewriter
 D  txd             Renderware TeXture Dictionary
 DE u16be           PCM unsigned 16-bit big-endian
 DE u16le           PCM unsigned 16-bit little-endian
 DE u24be           PCM unsigned 24-bit big-endian
 DE u24le           PCM unsigned 24-bit little-endian
 DE u32be           PCM unsigned 32-bit big-endian
 DE u32le           PCM unsigned 32-bit little-endian
 DE u8              PCM unsigned 8-bit
  E uncodedframecrc uncoded framecrc testing
 DE vc1             raw VC-1 video
 DE vc1test         VC-1 test bitstream
  E vcd             MPEG-1 Systems / MPEG program stream (VCD)
 D  vfwcap          VfW video capture
 D  vivo            Vivo
 D  vmd             Sierra VMD
  E vob             MPEG-2 PS (VOB)
 D  vobsub          VobSub subtitle format
 DE voc             Creative Voice
 D  vplayer         VPlayer subtitles
 D  vqf             Nippon Telegraph and Telephone Corporation (NTT) TwinVQ
 DE w64             Sony Wave64
 DE wav             WAV / WAVE (Waveform Audio)
 D  wc3movie        Wing Commander III movie
  E webm            WebM
 DE webm_dash_manifest WebM DASH Manifest
  E webp            WebP
 D  webp_pipe       piped webp sequence
 DE webvtt          WebVTT subtitle
 D  wsaud           Westwood Studios audio
 D  wsvqa           Westwood Studios VQA
 DE wtv             Windows Television (WTV)
 DE wv              raw WavPack
 D  xa              Maxis XA
 D  xbin            eXtended BINary text (XBIN)
 D  xmv             Microsoft XMV
 D  xwma            Microsoft xWMA
 D  yop             Psygnosis YOP
 DE yuv4mpegpipe    YUV4MPEG pipe
";

            var result = ss.Split('\r').Select(l => l.Trim('\n')).ToList();


            var kk= result.FindIndex(l => l.Trim() == "--");

            var v= result.Skip(kk+1).ToList();

            string[] arr = new string[] { "    " };

            foreach (var item in v)
            {
                var cc = item.Split(arr,StringSplitOptions.RemoveEmptyEntries);
            }

            var formats = FFmpegService.Instance.GetFormats();

        }

        [TestMethod]
        public void TestMethod2()
        {

            string filePath = @"F:\GitHub\WPF-MediaConverter\Document\from.mp4";


            //string ss = FFmpegService.Instance.GetDetail(filePath);


            FFmpegService.Instance.GetMediaEntity(filePath);



        }

        [TestMethod]
        public void TestMethod3()
        {
            string ss = @"ffmpeg version 2.5.3 Copyright (c) 2000-2015 the FFmpeg developers
  built on Aug  3 2015 16:53:06 with gcc 4.9.1 (GCC)
  configuration: --arch=x86 --cross-prefix=/home/builder/mingw-w64-i686-3.6.4/mingw-w64-i686/mingw-w64-i686/bin/i686-w64-mingw32- --target-os=mingw32 --pkg-config=pkg-config --disable-static --enable-shared --enable-gpl --enable-version3 --disable-w32threads --enable-avisynth --enable-bzlib --enable-fontconfig --enable-frei0r --enable-gnutls --enable-iconv --enable-libass --enable-libbluray --enable-libbs2b --enable-libcaca --enable-libfreetype --enable-libgsm --enable-libilbc --enable-libmp3lame --enable-libopencore-amrnb --enable-libopencore-amrwb --enable-libopus --enable-librtmp --enable-libschroedinger --enable-libsoxr --enable-libspeex --enable-libtheora --enable-libtwolame --enable-libvidstab --enable-libvo-aacenc --enable-libvorbis --enable-libvpx --enable-libwavpack --enable-libwebp --enable-libx264 --enable-libxavs --enable-libxvid --enable-lzma --enable-zlib --enable-libfdk-aac --enable-nonfree --enable-dxva2 --disable-sse --disable-sse2 --disable-amd3dnow --disable-amd3dnowext --disable-sse3 --disable-ssse3 --disable-sse4 --disable-sse42 --disable-avx --disable-os2threads --disable-ffplay --disable-doc --disable-htmlpages --disable-manpages --disable-podpages --disable-txtpages --prefix=/home/builder/ffmpeg-i686-mw364-trunk --extra-cflags='-m32 -I/home/builder/mingw-w64-i686-3.6.4/mingw-w64-i686/mingw-w64-i686/include' --extra-ldflags='-L/home/builder/mingw-w64-i686-3.6.4/mingw-w64-i686/mingw-w64-i686/lib -L/home/builder/mingw-w64-i686-3.6.4/mingw-w64-i686/mingw-w64-i686/bin -static-libgcc -static -lpthread'
  libavutil      54. 15.100 / 54. 15.100
  libavcodec     56. 13.100 / 56. 13.100
  libavformat    56. 15.102 / 56. 15.102
  libavdevice    56.  3.100 / 56.  3.100
  libavfilter     5.  2.103 /  5.  2.103
  libswscale      3.  1.101 /  3.  1.101
  libswresample   1.  1.100 /  1.  1.100
  libpostproc    53.  3.100 / 53.  3.100
Codecs:
 D..... = Decoding supported
 .E.... = Encoding supported
 ..V... = Video codec
 ..A... = Audio codec
 ..S... = Subtitle codec
 ...I.. = Intra frame-only codec
 ....L. = Lossy compression
 .....S = Lossless compression
 -------
 D.VI.. 012v                 Uncompressed 4:2:2 10-bit
 D.V.L. 4xm                  4X Movie
 D.VI.S 8bps                 QuickTime 8BPS video
 .EVIL. a64_multi            Multicolor charset for Commodore 64 (encoders: a64multi )
 .EVIL. a64_multi5           Multicolor charset for Commodore 64, extended with 5th color (colram) (encoders: a64multi5 )
 D.V..S aasc                 Autodesk RLE
 D.VIL. aic                  Apple Intermediate Codec
 DEVI.S alias_pix            Alias/Wavefront PIX image
 DEVIL. amv                  AMV Video
 D.V.L. anm                  Deluxe Paint Animation
 D.V.L. ansi                 ASCII/ANSI art
 D.V..S apng                 APNG (Animated Portable Network Graphics) image
 DEVIL. asv1                 ASUS V1
 DEVIL. asv2                 ASUS V2
 D.VIL. aura                 Auravision AURA
 D.VIL. aura2                Auravision Aura 2
 D.V... avrn                 Avid AVI Codec
 DEVI.. avrp                 Avid 1:1 10-bit RGB Packer
 D.V.L. avs                  AVS (Audio Video Standard) video
 DEVI.. avui                 Avid Meridien Uncompressed
 DEVI.. ayuv                 Uncompressed packed MS 4:4:4:4
 D.V.L. bethsoftvid          Bethesda VID video
 D.V.L. bfi                  Brute Force & Ignorance
 D.V.L. binkvideo            Bink video
 D.VI.. bintext              Binary text
 DEVI.S bmp                  BMP (Windows and OS/2 bitmap)
 D.V..S bmv_video            Discworld II BMV video
 D.VI.S brender_pix          BRender PIX image
 D.V.L. c93                  Interplay C93
 DEV.L. cavs                 Chinese AVS (Audio Video Standard) (AVS1-P2, JiZhun profile) (encoders: libxavs )
 D.V.L. cdgraphics           CD Graphics video
 D.VIL. cdxl                 Commodore CDXL video
 DEV.L. cinepak              Cinepak
 DEVIL. cljr                 Cirrus Logic AccuPak
 D.VI.S cllc                 Canopus Lossless Codec
 D.V.L. cmv                  Electronic Arts CMV video (decoders: eacmv )
 D.V... cpia                 CPiA video format
 D.V..S cscd                 CamStudio (decoders: camstudio )
 D.VIL. cyuv                 Creative YUV (CYUV)
 D.V.L. dfa                  Chronomaster DFA
 DEV.LS dirac                Dirac (decoders: dirac libschroedinger ) (encoders: libschroedinger )
 DEVIL. dnxhd                VC3/DNxHD
 DEVI.S dpx                  DPX (Digital Picture Exchange) image
 D.V.L. dsicinvideo          Delphine Software International CIN video
 DEVIL. dvvideo              DV (Digital Video)
 D.V..S dxa                  Feeble Files/ScummVM DXA
 D.VI.S dxtory               Dxtory
 D.V.L. escape124            Escape 124
 D.V.L. escape130            Escape 130
 D.VILS exr                  OpenEXR image
 DEV..S ffv1                 FFmpeg video codec #1
 DEVI.S ffvhuff              Huffyuv FFmpeg variant
 D.V.L. fic                  Mirillis FIC
 DEV..S flashsv              Flash Screen Video v1
 DEV.L. flashsv2             Flash Screen Video v2
 D.V..S flic                 Autodesk Animator Flic video
 DEV.L. flv1                 FLV / Sorenson Spark / Sorenson H.263 (Flash Video) (decoders: flv ) (encoders: flv )
 D.V..S fraps                Fraps
 D.VI.S frwu                 Forward Uncompressed
 D.V.L. g2m                  Go2Meeting
 DEV..S gif                  GIF (Graphics Interchange Format)
 DEV.L. h261                 H.261
 DEV.L. h263                 H.263 / H.263-1996, H.263+ / H.263-1998 / H.263 version 2
 D.V.L. h263i                Intel H.263
 DEV.L. h263p                H.263+ / H.263-1998 / H.263 version 2
 DEV.LS h264                 H.264 / AVC / MPEG-4 AVC / MPEG-4 part 10 (encoders: libx264 libx264rgb )
 D.V.L. hevc                 H.265 / HEVC (High Efficiency Video Coding)
 D.V.L. hnm4video            HNM 4 video
 DEVI.S huffyuv              HuffYUV
 D.V.L. idcin                id Quake II CIN video (decoders: idcinvideo )
 D.VI.. idf                  iCEDraw text
 D.V.L. iff_byterun1         IFF ByteRun1 (decoders: iff )
 D.V.L. iff_ilbm             IFF ILBM (decoders: iff )
 D.V.L. indeo2               Intel Indeo 2
 D.V.L. indeo3               Intel Indeo 3
 D.V.L. indeo4               Intel Indeo Video Interactive 4
 D.V.L. indeo5               Intel Indeo Video Interactive 5
 D.V.L. interplayvideo       Interplay MVE video
 DEVILS jpeg2000             JPEG 2000
 DEVILS jpegls               JPEG-LS
 D.VIL. jv                   Bitmap Brothers JV video
 D.V.L. kgv1                 Kega Game Video
 D.V.L. kmvc                 Karl Morton's video codec
 D.VI.S lagarith             Lagarith lossless
 .EVI.S ljpeg                Lossless JPEG
 D.VI.S loco                 LOCO
 D.V.L. mad                  Electronic Arts Madcow Video (decoders: eamad )
 D.VIL. mdec                 Sony PlayStation MDEC (Motion DECoder)
 D.V.L. mimic                Mimic
 DEVIL. mjpeg                Motion JPEG
 D.VIL. mjpegb               Apple MJPEG-B
 D.V.L. mmvideo              American Laser Games MM Video
 D.V.L. motionpixels         Motion Pixels video
 DEV.L. mpeg1video           MPEG-1 video
 DEV.L. mpeg2video           MPEG-2 video (decoders: mpeg2video mpegvideo )
 DEV.L. mpeg4                MPEG-4 part 2 (encoders: mpeg4 libxvid )
 ..V.L. mpegvideo_xvmc       MPEG-1/2 video XvMC (X-Video Motion Compensation)
 D.V.L. msa1                 MS ATC Screen
 D.V.L. msmpeg4v1            MPEG-4 part 2 Microsoft variant version 1
 DEV.L. msmpeg4v2            MPEG-4 part 2 Microsoft variant version 2
 DEV.L. msmpeg4v3            MPEG-4 part 2 Microsoft variant version 3 (decoders: msmpeg4 ) (encoders: msmpeg4 )
 D.V..S msrle                Microsoft RLE
 D.V.L. mss1                 MS Screen 1
 D.VIL. mss2                 MS Windows Media Video V9 Screen
 DEV.L. msvideo1             Microsoft Video 1
 D.VI.S mszh                 LCL (LossLess Codec Library) MSZH
 D.V.L. mts2                 MS Expression Encoder Screen
 D.VIL. mvc1                 Silicon Graphics Motion Video Compressor 1
 D.VIL. mvc2                 Silicon Graphics Motion Video Compressor 2
 D.V.L. mxpeg                Mobotix MxPEG video
 D.V.L. nuv                  NuppelVideo/RTJPEG
 D.V.L. paf_video            Amazing Studio Packed Animation File Video
 DEVI.S pam                  PAM (Portable AnyMap) image
 DEVI.S pbm                  PBM (Portable BitMap) image
 DEVI.S pcx                  PC Paintbrush PCX image
 DEVI.S pgm                  PGM (Portable GrayMap) image
 DEVI.S pgmyuv               PGMYUV (Portable GrayMap YUV) image
 D.VIL. pictor               Pictor/PC Paint
 DEV..S png                  PNG (Portable Network Graphics) image
 DEVI.S ppm                  PPM (Portable PixelMap) image
 DEVIL. prores               Apple ProRes (iCodec Pro) (decoders: prores prores_lgpl ) (encoders: prores prores_aw prores_ks )
 D.VIL. ptx                  V.Flash PTX image
 D.VI.S qdraw                Apple QuickDraw
 D.V.L. qpeg                 Q-team QPEG
 DEV..S qtrle                QuickTime Animation (RLE) video
 DEVI.S r10k                 AJA Kona 10-bit RGB Codec
 DEVI.S r210                 Uncompressed RGB 10-bit
 DEVI.S rawvideo             raw video
 D.VIL. rl2                  RL2 video
 DEV.L. roq                  id RoQ video (decoders: roqvideo ) (encoders: roqvideo )
 D.V.L. rpza                 QuickTime video (RPZA)
 DEV.L. rv10                 RealVideo 1.0
 DEV.L. rv20                 RealVideo 2.0
 D.V.L. rv30                 RealVideo 3.0
 D.V.L. rv40                 RealVideo 4.0
 D.V.L. sanm                 LucasArts SANM/SMUSH video
 DEVI.S sgi                  SGI image
 D.VI.S sgirle               SGI RLE 8-bit
 D.V.L. smackvideo           Smacker video (decoders: smackvid )
 D.V.L. smc                  QuickTime Graphics (SMC)
 D.V... smv                  Sigmatel Motion Video (decoders: smvjpeg )
 DEV.LS snow                 Snow
 D.VIL. sp5x                 Sunplus JPEG (SP5X)
 DEVI.S sunrast              Sun Rasterfile image
 DEV.L. svq1                 Sorenson Vector Quantizer 1 / Sorenson Video 1 / SVQ1
 D.V.L. svq3                 Sorenson Vector Quantizer 3 / Sorenson Video 3 / SVQ3
 DEVI.S targa                Truevision Targa image
 D.VI.. targa_y216           Pinnacle TARGA CineWave YUV16
 D.V.L. tgq                  Electronic Arts TGQ video (decoders: eatgq )
 D.V.L. tgv                  Electronic Arts TGV video (decoders: eatgv )
 DEV.L. theora               Theora (encoders: libtheora )
 D.VIL. thp                  Nintendo Gamecube THP video
 D.V.L. tiertexseqvideo      Tiertex Limited SEQ video
 DEVI.S tiff                 TIFF image
 D.VIL. tmv                  8088flex TMV
 D.V.L. tqi                  Electronic Arts TQI video (decoders: eatqi )
 D.V.L. truemotion1          Duck TrueMotion 1.0
 D.V.L. truemotion2          Duck TrueMotion 2.0
 D.V..S tscc                 TechSmith Screen Capture Codec (decoders: camtasia )
 D.V.L. tscc2                TechSmith Screen Codec 2
 D.VIL. txd                  Renderware TXD (TeXture Dictionary) image
 D.V.L. ulti                 IBM UltiMotion (decoders: ultimotion )
 DEVI.S utvideo              Ut Video
 DEVI.S v210                 Uncompressed 4:2:2 10-bit
 D.VI.S v210x                Uncompressed 4:2:2 10-bit
 DEVI.. v308                 Uncompressed packed 4:4:4
 DEVI.. v408                 Uncompressed packed QT 4:4:4:4
 DEVI.S v410                 Uncompressed 4:4:4 10-bit
 D.V.L. vb                   Beam Software VB
 D.VI.S vble                 VBLE Lossless Codec
 D.V.L. vc1                  SMPTE VC-1
 D.V.L. vc1image             Windows Media Video 9 Image v2
 D.VIL. vcr1                 ATI VCR1
 D.VIL. vixl                 Miro VideoXL (decoders: xl )
 D.V.L. vmdvideo             Sierra VMD video
 D.V..S vmnc                 VMware Screen Codec / VMware Video
 D.V.L. vp3                  On2 VP3
 D.V.L. vp5                  On2 VP5
 D.V.L. vp6                  On2 VP6
 D.V.L. vp6a                 On2 VP6 (Flash version, with alpha channel)
 D.V.L. vp6f                 On2 VP6 (Flash version)
 D.V.L. vp7                  On2 VP7
 DEV.L. vp8                  On2 VP8 (decoders: vp8 libvpx ) (encoders: libvpx )
 DEV.L. vp9                  Google VP9 (decoders: vp9 libvpx-vp9 ) (encoders: libvpx-vp9 )
 DEVILS webp                 WebP (encoders: libwebp )
 DEV.L. wmv1                 Windows Media Video 7
 DEV.L. wmv2                 Windows Media Video 8
 D.V.L. wmv3                 Windows Media Video 9
 D.V.L. wmv3image            Windows Media Video 9 Image
 D.VIL. wnv1                 Winnov WNV1
 D.V.L. ws_vqa               Westwood Studios VQA (Vector Quantized Animation) video (decoders: vqavideo )
 D.V.L. xan_wc3              Wing Commander III / Xan
 D.V.L. xan_wc4              Wing Commander IV / Xxan
 D.VI.. xbin                 eXtended BINary text
 DEVI.S xbm                  XBM (X BitMap) image
 DEVIL. xface                X-face image
 DEVI.S xwd                  XWD (X Window Dump) image
 DEVI.. y41p                 Uncompressed YUV 4:1:1 12-bit
 D.V.L. yop                  Psygnosis YOP Video
 DEVI.. yuv4                 Uncompressed packed 4:2:0
 D.V..S zerocodec            ZeroCodec Lossless Video
 DEVI.S zlib                 LCL (LossLess Codec Library) ZLIB
 DEV..S zmbv                 Zip Motion Blocks Video
 D.A.L. 8svx_exp             8SVX exponential
 D.A.L. 8svx_fib             8SVX fibonacci
 DEA.L. aac                  AAC (Advanced Audio Coding) (decoders: aac libfdk_aac ) (encoders: aac libfdk_aac libvo_aacenc )
 D.A.L. aac_latm             AAC LATM (Advanced Audio Coding LATM syntax)
 DEA.L. ac3                  ATSC A/52A (AC-3) (decoders: ac3 ac3_fixed ) (encoders: ac3 ac3_fixed )
 D.A.L. adpcm_4xm            ADPCM 4X Movie
 DEA.L. adpcm_adx            SEGA CRI ADX ADPCM
 D.A.L. adpcm_afc            ADPCM Nintendo Gamecube AFC
 D.A.L. adpcm_ct             ADPCM Creative Technology
 D.A.L. adpcm_dtk            ADPCM Nintendo Gamecube DTK
 D.A.L. adpcm_ea             ADPCM Electronic Arts
 D.A.L. adpcm_ea_maxis_xa    ADPCM Electronic Arts Maxis CDROM XA
 D.A.L. adpcm_ea_r1          ADPCM Electronic Arts R1
 D.A.L. adpcm_ea_r2          ADPCM Electronic Arts R2
 D.A.L. adpcm_ea_r3          ADPCM Electronic Arts R3
 D.A.L. adpcm_ea_xas         ADPCM Electronic Arts XAS
 DEA.L. adpcm_g722           G.722 ADPCM (decoders: g722 ) (encoders: g722 )
 DEA.L. adpcm_g726           G.726 ADPCM (decoders: g726 ) (encoders: g726 )
 D.A.L. adpcm_g726le         G.726 ADPCM little-endian (decoders: g726le )
 D.A.L. adpcm_ima_amv        ADPCM IMA AMV
 D.A.L. adpcm_ima_apc        ADPCM IMA CRYO APC
 D.A.L. adpcm_ima_dk3        ADPCM IMA Duck DK3
 D.A.L. adpcm_ima_dk4        ADPCM IMA Duck DK4
 D.A.L. adpcm_ima_ea_eacs    ADPCM IMA Electronic Arts EACS
 D.A.L. adpcm_ima_ea_sead    ADPCM IMA Electronic Arts SEAD
 D.A.L. adpcm_ima_iss        ADPCM IMA Funcom ISS
 D.A.L. adpcm_ima_oki        ADPCM IMA Dialogic OKI
 DEA.L. adpcm_ima_qt         ADPCM IMA QuickTime
 D.A.L. adpcm_ima_rad        ADPCM IMA Radical
 D.A.L. adpcm_ima_smjpeg     ADPCM IMA Loki SDL MJPEG
 DEA.L. adpcm_ima_wav        ADPCM IMA WAV
 D.A.L. adpcm_ima_ws         ADPCM IMA Westwood
 DEA.L. adpcm_ms             ADPCM Microsoft
 D.A.L. adpcm_sbpro_2        ADPCM Sound Blaster Pro 2-bit
 D.A.L. adpcm_sbpro_3        ADPCM Sound Blaster Pro 2.6-bit
 D.A.L. adpcm_sbpro_4        ADPCM Sound Blaster Pro 4-bit
 DEA.L. adpcm_swf            ADPCM Shockwave Flash
 D.A.L. adpcm_thp            ADPCM Nintendo Gamecube THP
 D.A.L. adpcm_vima           LucasArts VIMA audio (decoders: adpcm_vima vima )
 D.A.L. adpcm_xa             ADPCM CDROM XA
 DEA.L. adpcm_yamaha         ADPCM Yamaha
 DEA..S alac                 ALAC (Apple Lossless Audio Codec)
 DEA.L. amr_nb               AMR-NB (Adaptive Multi-Rate NarrowBand) (decoders: amrnb libopencore_amrnb ) (encoders: libopencore_amrnb )
 D.A.L. amr_wb               AMR-WB (Adaptive Multi-Rate WideBand) (decoders: amrwb libopencore_amrwb )
 D.A..S ape                  Monkey's Audio
 D.A.L. atrac1               ATRAC1 (Adaptive TRansform Acoustic Coding)
 D.A.L. atrac3               ATRAC3 (Adaptive TRansform Acoustic Coding 3)
 D.A.L. atrac3p              ATRAC3+ (Adaptive TRansform Acoustic Coding 3+) (decoders: atrac3plus )
 D.A.L. avc                  On2 Audio for Video Codec (decoders: on2avc )
 D.A.L. binkaudio_dct        Bink Audio (DCT)
 D.A.L. binkaudio_rdft       Bink Audio (RDFT)
 D.A.L. bmv_audio            Discworld II BMV audio
 ..A.L. celt                 Constrained Energy Lapped Transform (CELT)
 DEA.L. comfortnoise         RFC 3389 Comfort Noise
 D.A.L. cook                 Cook / Cooker / Gecko (RealAudio G2)
 D.A.L. dsd_lsbf             DSD (Direct Stream Digital), least significant bit first
 D.A.L. dsd_lsbf_planar      DSD (Direct Stream Digital), least significant bit first, planar
 D.A.L. dsd_msbf             DSD (Direct Stream Digital), most significant bit first
 D.A.L. dsd_msbf_planar      DSD (Direct Stream Digital), most significant bit first, planar
 D.A.L. dsicinaudio          Delphine Software International CIN audio
 DEA.LS dts                  DCA (DTS Coherent Acoustics) (decoders: dca ) (encoders: dca )
 ..A.L. dvaudio              DV audio
 DEA.L. eac3                 ATSC A/52B (AC-3, E-AC-3)
 D.A.L. evrc                 EVRC (Enhanced Variable Rate Codec)
 DEA..S flac                 FLAC (Free Lossless Audio Codec)
 DEA.L. g723_1               G.723.1
 D.A.L. g729                 G.729
 DEA.L. gsm                  GSM (decoders: gsm libgsm ) (encoders: libgsm )
 DEA.L. gsm_ms               GSM Microsoft variant (decoders: gsm_ms libgsm_ms ) (encoders: libgsm_ms )
 D.A.L. iac                  IAC (Indeo Audio Coder)
 DEA.L. ilbc                 iLBC (Internet Low Bitrate Codec) (decoders: libilbc ) (encoders: libilbc )
 D.A.L. imc                  IMC (Intel Music Coder)
 D.A.L. interplay_dpcm       DPCM Interplay
 D.A.L. mace3                MACE (Macintosh Audio Compression/Expansion) 3:1
 D.A.L. mace6                MACE (Macintosh Audio Compression/Expansion) 6:1
 D.A.L. metasound            Voxware MetaSound
 D.A..S mlp                  MLP (Meridian Lossless Packing)
 D.A.L. mp1                  MP1 (MPEG audio layer 1) (decoders: mp1 mp1float )
 DEA.L. mp2                  MP2 (MPEG audio layer 2) (decoders: mp2 mp2float ) (encoders: mp2 mp2fixed libtwolame )
 DEA.L. mp3                  MP3 (MPEG audio layer 3) (decoders: mp3 mp3float ) (encoders: libmp3lame )
 D.A.L. mp3adu               ADU (Application Data Unit) MP3 (MPEG audio layer 3) (decoders: mp3adu mp3adufloat )
 D.A.L. mp3on4               MP3onMP4 (decoders: mp3on4 mp3on4float )
 D.A..S mp4als               MPEG-4 Audio Lossless Coding (ALS) (decoders: als )
 D.A.L. musepack7            Musepack SV7 (decoders: mpc7 )
 D.A.L. musepack8            Musepack SV8 (decoders: mpc8 )
 DEA.L. nellymoser           Nellymoser Asao
 DEA.L. opus                 Opus (Opus Interactive Audio Codec) (decoders: opus libopus ) (encoders: libopus )
 D.A.L. paf_audio            Amazing Studio Packed Animation File Audio
 DEA.L. pcm_alaw             PCM A-law / G.711 A-law
 D.A..S pcm_bluray           PCM signed 16|20|24-bit big-endian for Blu-ray media
 D.A..S pcm_dvd              PCM signed 20|24-bit big-endian
 DEA..S pcm_f32be            PCM 32-bit floating point big-endian
 DEA..S pcm_f32le            PCM 32-bit floating point little-endian
 DEA..S pcm_f64be            PCM 64-bit floating point big-endian
 DEA..S pcm_f64le            PCM 64-bit floating point little-endian
 D.A..S pcm_lxf              PCM signed 20-bit little-endian planar
 DEA.L. pcm_mulaw            PCM mu-law / G.711 mu-law
 DEA..S pcm_s16be            PCM signed 16-bit big-endian
 DEA..S pcm_s16be_planar     PCM signed 16-bit big-endian planar
 DEA..S pcm_s16le            PCM signed 16-bit little-endian
 DEA..S pcm_s16le_planar     PCM signed 16-bit little-endian planar
 DEA..S pcm_s24be            PCM signed 24-bit big-endian
 DEA..S pcm_s24daud          PCM D-Cinema audio signed 24-bit
 DEA..S pcm_s24le            PCM signed 24-bit little-endian
 DEA..S pcm_s24le_planar     PCM signed 24-bit little-endian planar
 DEA..S pcm_s32be            PCM signed 32-bit big-endian
 DEA..S pcm_s32le            PCM signed 32-bit little-endian
 DEA..S pcm_s32le_planar     PCM signed 32-bit little-endian planar
 DEA..S pcm_s8               PCM signed 8-bit
 DEA..S pcm_s8_planar        PCM signed 8-bit planar
 DEA..S pcm_u16be            PCM unsigned 16-bit big-endian
 DEA..S pcm_u16le            PCM unsigned 16-bit little-endian
 DEA..S pcm_u24be            PCM unsigned 24-bit big-endian
 DEA..S pcm_u24le            PCM unsigned 24-bit little-endian
 DEA..S pcm_u32be            PCM unsigned 32-bit big-endian
 DEA..S pcm_u32le            PCM unsigned 32-bit little-endian
 DEA..S pcm_u8               PCM unsigned 8-bit
 D.A.L. pcm_zork             PCM Zork
 D.A.L. qcelp                QCELP / PureVoice
 D.A.L. qdm2                 QDesign Music Codec 2
 ..A.L. qdmc                 QDesign Music
 DEA.L. ra_144               RealAudio 1.0 (14.4K) (decoders: real_144 ) (encoders: real_144 )
 D.A.L. ra_288               RealAudio 2.0 (28.8K) (decoders: real_288 )
 D.A..S ralf                 RealAudio Lossless
 DEA.L. roq_dpcm             DPCM id RoQ
 DEA..S s302m                SMPTE 302M
 D.A..S shorten              Shorten
 D.A.L. sipr                 RealAudio SIPR / ACELP.NET
 D.A.L. smackaudio           Smacker audio (decoders: smackaud )
 ..A.L. smv                  SMV (Selectable Mode Vocoder)
 D.A.L. sol_dpcm             DPCM Sol
 DEA... sonic                Sonic
 .EA... sonicls              Sonic lossless
 DEA.L. speex                Speex (decoders: libspeex ) (encoders: libspeex )
 D.A..S tak                  TAK (Tom's lossless Audio Kompressor)
 D.A..S truehd               TrueHD
 D.A.L. truespeech           DSP Group TrueSpeech
 DEA..S tta                  TTA (True Audio)
 D.A.L. twinvq               VQF TwinVQ
 D.A.L. vima                 LucasArts VIMA audio (deprecated id) (decoders: adpcm_vima vima )
 D.A.L. vmdaudio             Sierra VMD audio
 DEA.L. vorbis               Vorbis (decoders: vorbis libvorbis ) (encoders: vorbis libvorbis )
 ..A.L. voxware              Voxware RT29 Metasound
 D.A... wavesynth            Wave synthesis pseudo-codec
 DEA.LS wavpack              WavPack (encoders: wavpack libwavpack )
 D.A.L. westwood_snd1        Westwood Audio (SND1) (decoders: ws_snd1 )
 D.A..S wmalossless          Windows Media Audio Lossless
 D.A.L. wmapro               Windows Media Audio 9 Professional
 DEA.L. wmav1                Windows Media Audio 1
 DEA.L. wmav2                Windows Media Audio 2
 D.A.L. wmavoice             Windows Media Audio Voice
 D.A.L. xan_dpcm             DPCM Xan
 ..D... bin_data             binary data
 ..D... dvd_nav_packet       DVD Nav packet
 ..D... klv                  SMPTE 336M Key-Length-Value (KLV) metadata
 ..D... otf                  OpenType font
 ..D... timed_id3            timed ID3 metadata
 ..D... ttf                  TrueType font
 DES... ass                  ASS (Advanced SSA) subtitle
 DES... dvb_subtitle         DVB subtitles (decoders: dvbsub ) (encoders: dvbsub )
 ..S... dvb_teletext         DVB teletext
 DES... dvd_subtitle         DVD subtitles (decoders: dvdsub ) (encoders: dvdsub )
 ..S... eia_608              EIA-608 closed captions
 D.S... hdmv_pgs_subtitle    HDMV Presentation Graphic Stream subtitles (decoders: pgssub )
 D.S... jacosub              JACOsub subtitle
 D.S... microdvd             MicroDVD subtitle
 DES... mov_text             MOV text
 D.S... mpl2                 MPL2 subtitle
 D.S... pjs                  PJS (Phoenix Japanimation Society) subtitle
 D.S... realtext             RealText subtitle
 D.S... sami                 SAMI subtitle
 ..S... srt                  SubRip subtitle with embedded timing
 DES... ssa                  SSA (SubStation Alpha) subtitle
 D.S... stl                  Spruce subtitle format
 DES... subrip               SubRip subtitle (decoders: srt subrip ) (encoders: srt subrip )
 D.S... subviewer            SubViewer subtitle
 D.S... subviewer1           SubViewer v1 subtitle
 D.S... text                 raw UTF-8 text
 D.S... vplayer              VPlayer subtitle
 DES... webvtt               WebVTT subtitle
 DES... xsub                 XSUB
";

            var result = ss.Split('\r').Select(l => l.Trim('\n')).ToList();


            var kk = result.FindIndex(l => l.Trim() == "-------");

            var v = result.Skip(kk + 1).ToList();

            string[] arr = new string[] { "    " };

            foreach (var item in v)
            {
                var cc = item.Split(arr, StringSplitOptions.RemoveEmptyEntries);
            }

            var formats = FFmpegService.Instance.GetCodecs();

        }


    }
}
