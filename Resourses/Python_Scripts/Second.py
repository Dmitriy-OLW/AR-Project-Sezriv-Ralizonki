import pydicom 
from pydicom.dataset import FileDataset, FileMetaDataset 
import numpy as np 
import cv2 
import os 
 
 
def create_dicom_from_image(image_path, output_dcm_path): 
    # Читаем изображение и конвертируем в оттенки серого (если 
нужно) 
    img = cv2.imread(image_path, cv2.IMREAD_GRAYSCALE) 
 
    # Создаем DICOM-файл 
    ds = FileDataset(output_dcm_path, {}, 
file_meta=FileMetaDataset(), preamble=b"\0" * 128) 
    ds.PixelData = img.tobytes() 
    ds.Rows, ds.Columns = img.shape 
    ds.BitsAllocated = 8 
    ds.BitsStored = 8 
    ds.HighBit = 7 
    ds.PixelRepresentation = 0 
    ds.PhotometricInterpretation = "MONOCHROME1" 
    ds.SamplesPerPixel = 1 
    ds.PatientName = "Anonymous" 
    ds.PatientID = "12345" 
    ds.Modality = "OT"  # Other 
ds.save_as(output_dcm_path) 
# Конвертируем все кадры 
for i in range(154):  # Замените на реальное число кадров – 
число полученных кадров в первом скрипте 
img_path = f"frame_{i:04d}.png" 
dcm_path = f"frame_{i:04d}.dcm" 
if os.path.exists(img_path): 
create_dicom_from_image(img_path, dcm_path)