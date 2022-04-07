#!/bin/bash
for i in *.mtl
do
	mv -v "$i" ".$i"
done
