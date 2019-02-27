#!/usr/bin/env python2
# coding=utf-8

IP_CAM = "localhost:9876"

import numpy as np
import cv2 as cv
import urllib
from PIL import Image

def count_white_pixels(img):
    """Nombre de pixel blanc dans l'iamge"""
    nb_white_pixel = 0
    for row in range(img.shape[0]):
        for column in range(img.shape[1]):
            if img[row, column] == 255:
                nb_white_pixel += 1 
    print nb_white_pixel
    return nb_white_pixel


def local_ouvert():
    """"Print le status de l'ouverture de local d'epitanime"""
    urllib.urlretrieve("http://" + IP_CAM +
                               "/snapshot.cgi?user=admin&amp;pwd=adm42tanime",
                               "localnow.jpg")
    img = cv.imread('localnow.jpg', cv.IMREAD_GRAYSCALE)
    ret, new_img = cv.threshold(img, 127, 255, cv.THRESH_BINARY)

    cv.imshow('local_image', new_img)
    nb_pixel = new_img.shape[0] * new_img.shape[1]
    nb_white_pixel = count_white_pixels(new_img)
    cv.waitKey(0)
    cv.destroyAllWindows()
    return ((nb_white_pixel / nb_pixel) > 0.16)

if __name__ == "__main__":
    if local_ouvert():
        print "Le local est ouvert!"
    else:
        print "Le local est fermé!"