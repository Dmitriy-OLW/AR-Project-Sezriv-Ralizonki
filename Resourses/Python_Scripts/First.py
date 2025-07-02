import cv2 
#путь до обрабатываемого файла 
video = 
cv2.VideoCapture("C:\\Users\\ivans\\PycharmProjects\\PythonProj
 ect2\\r\\19SMP.avi")#замените путь на необходимый файл 
success, frame = video.read() 
count = 0 
while success: 
    cv2.imwrite(f"frame_{count:04d}.png", frame) 
    success, frame = video.read() 
    count += 1 
    #получение файлов 