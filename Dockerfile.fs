FROM i386/debian:stable

LABEL maintainer="jonni.rainisto@gmail.com"

ENV PATH /opt/m68k-amigaos/bin:$PATH

RUN apt-get update

RUN DEBIAN_FRONTEND=noninteractive apt-get -y install git make nano vim gcc-arm-none-eabi \
    srecord stm32flash python-crcmod zip autoconf bison flex g++ gcc gettext gperf libgmp10 \
    libgmpxx4ldbl libgmp-dev libmpc3 libmpc-dev libmpfr4 libmpfr-dev libncurses5-dev python-dev

RUN cd /root && git clone https://github.com/bebbo/amigaos-cross-toolchain.git && \
    cd /root/amigaos-cross-toolchain && ./toolchain-m68k --prefix=/opt/m68k-amigaos --threads=4 build || true

ADD hxcfe/hxcfe /usr/bin/hxcfe
ADD hxcfe/*.so /usr/local/lib/

RUN ldconfig && git clone https://github.com/rainisto/FlashFloppy_File_Selector.git && \
    cd FlashFloppy_File_Selector/amiga && make

CMD (cd /FlashFloppy_File_Selector/amiga && cp AUTOBOOT* /output/)
