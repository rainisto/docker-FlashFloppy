#!/bin/bash
# give --no-cache as parameter if you want to force rebuild
BASE=$(pwd)
docker build $1 -t gotek_ff -f Dockerfile.ff .
docker build $1 -t gotek_fs -f Dockerfile.fs .
docker build $1 -t gotek_fs_keirf -f Dockerfile.fs.keirf .

docker run -v $BASE/output:/output -t gotek_ff
docker run -v $BASE/output:/output -t gotek_fs
docker run -v $BASE/output_keirf:/output -t gotek_fs_keirf
