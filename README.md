<h2>Документация решения</h2>

**Ссылка на приложение:** https://dmitriy-olw.itch.io/ar-mr-app-for-medical-files

### **О проекте**

**Название:** AR-приложение для визуализации фантома щитовидной железы.

**Цель проекта:** Создание интерактивного AR-приложения для визуализации 3D-моделей и медицинских сканов (МРТ, КТ, УЗИ) фантома щитовидной железы с возможностью управления через трекинг рук на гарнитуре Quest 3.

**Демонстрация работы приложения:** 
[![Видео-Демонстрация](https://youtu.be/K7Ahoak30Ks)](https://youtu.be/K7Ahoak30Ks)

**Скриншоты из приложения**:

<div align="center">
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/Preview.png" height="100" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(1).png" height="100" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(2).png" height="100" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(5).png" height="100" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(6).png" height="100" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(7).png" height="100" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(9).png" height="100" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(10).png" height="100" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(12).png" height="100" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(13).png" height="100" />
</div>


#### Приложение разработано в рамках Хакатона "AR для фантома щитовидной железы" Пятой Открытой конференции молодых учёных Центра диагностики и телемедицины. **Представленный проект занял второе место.** Приложение разработанно комндой "Сезрыв Рализёнки", участники команды: Константинов Дмитрий, Морев Олег, Пищагин Максим, Семичев Иван.

### **Диплом подтверждающий занятое место:**

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Sertifikat/Sertificat_.jpg" height="500">

<br>

### **Тестовые данные:** Resourses/Тестовые данные/



### **Ключевые особенности:**

- **Управление руками:** Использование трекинга рук и жестов для взаимодействия с интерфейсом.
- **Визуализация данных:**
  - Отображение 3D-моделей органов (фантома щитовидной железы, органы шеи, верхней части туловища и головы);
  - Загрузка и обработка медицинских сканов (DICOM / .dcm – папка, в которой содержатся эти данные) с настройкой рендеринга (прозрачность, изоповерхности), настройка срезов и настройка пропускаемости света и излучения;
  - Режимы визуализации: анатомический атлас, схематичное представление.
- **AR-режим:** Совмещение 3D-моделей с реальным окружением через Passthrough-камеру Quest 3.
- Интеграция Quest 3 как устройства визуализации с ПК в роли вычислительного устройства.

**Применение:** Обучение медиков, диагностика, презентации медицинских данных в AR.

1. **Описание подхода, используемых методов**

   - **Подход:** Использование трекинга рук и жестов для управления AR приложения. Использование компьютера как станции загрузки, обработки и вычисления данных, а гарнитуру **Quest 3**, как устройство визуализации приложения, визуализации пропускаемого реального мира и ввода и управлении приложением с помощь рук и жестов.

   - **Методы:**
     - Работа с Датасетом: **UnityVolumeRendering**;
     - Трекинг рук, жестов, управление UI элементами, настройка визуализации моделей и сканов, и основной функционал AR приложения: **MetaSDK**.
   - **Архитектура	модели:**	Использование	методов	объектно- ориентированного программирования.

2. **Указание ресурсов, используемых для разработки решения**

   - **Технические ресурсы:**

     - Движок: **Unity 2022.3.59f1**;
     - AR-платформа: **MetaSDK**;
     - Работа	с	Датасетом	-	сканами:	**UnityVolumeRendering**;
     - редобработка УЗИ: написанные Python скрипты (см. Resourses/Python_Scripts/).
 
  - **Данные:**

    - **Sketchfab**: 3D-модели;
    - **Asset Store**: SDK;
    - **Предоставляемые данные**: Датасеты сканов МРТ, УЗИ, КТ; 3D- модели фантома щитовидной железы;
    - **Предобработка данных УЗИ** с помощью Python скриптов, для формирования датасета с DICOM файлами (см. Resourses/Python_Scripts/).

  - **Инструменты:**
    - **Unity 2022.3.59f1;**
    - **Blender;**
    - **GitHub;**
    - **JetBrains Rider;**
    - **Visual Studio;**
    - **JetBrains PyCharm;**
    - **MetaSDK;**
    - **UnityVolumeRendering.**

3. **Необходимые зависимости и инструкции по их установке:**

   - Для доступа к Passthrough-камере Quest 3 с ПК необходимо включить функции среды разработчиков в Meta Quest Link (см. рис. 2.1). Для доступа к этим функциям необходимо выполнить двухфакторную аутентификацию на своём аккаунте Meta и создать на этом аккаунте организацию
   - В ходе разработки был изменён исходный код MetaSDK, для корректной работы проекта, при открытие его в движке Unity, необходимо повторно его изменить в проекте. В проекте необходимо найти скрипт: SpatialAnchorSpawnerBuildingBlock, и заменить его содержимое на код из /Resourses/Other/. На сцене MainScene, в поле инспекторе для этого скрипта должны быть значения, указанные на рисунке 2.2.
   - Для корректной работы загрузки датасета при открытии проекта с помощью движка Unity, необходимо переместить в проект файл: «SimpleITKCSharpNative.dll»	в	директорию: «..\Assets\UnityVolumeRendering\3rdparty\SimpleITK\» . После чего в верхнем меню Unity, найти вкладку «Volume Rendering», и в ней нажать на кнопку «Settings», после чего проверить в открывшемся меню, на нижней кнопке должно быть написано «Disable SimpleITK», если написано «EnabIe SimpleITk» нажмите на кнопку и убидитесь, что после загрузки название кнопки изменилось на «Disable SimpleITK» (см. рис. 2.3). Файл «SimpleITKCSharpNative.dll» приложен к файлам проекта.  (Файл находится в архиве, его необходимо предварительно разорхевировать, файл находится в Resourses/Other/SimpleITKCSharpNative.7z)

### **Инструкция по использованию проекта**

При загрузке пользователь видит перед собой меню с кнопкой «Новая точка привязки» (см. рис. 1), при нажатии на которую, на левой руке перед указательным пальцем появится модель фантома щитовидной железы.

Рисунок 1. Меню новой точки привязки:

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(9).jpeg" height="200">

Для пользователя необходимо переместить модель к фантому щитовидной железы в реальном мир и выставить модель так, чтобы она совпадала по положению с реальным фантомом. Далее необходимо на правой руке сделать жест: соединить большой палец и указательный палец. Модель зафиксирует своё расположение на фантоме, и пользователю откроется главное меню для управления основами функциями приложения.

В главном меню, можно выбрать перезаписать точку привязки и перейти в режим отображения (см. рис. 2)

Рисунок 2. Главное меню:

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(10).jpeg" height="200">

**В меню режима отображения** пользователю предоставляется три варианта отображения (см. рис. 3).

Рисунок 3. Меню режима отображения без загруженного датасета:

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(11).jpeg" height="200">

**Первое это отображение датасета - фантома щитовидной железы используя сканы: МРТ, КТ и УЗИ.** Перед отображением скана, необходимо загрузить его, для этого в основном меню необходимо перейти по кнопке

«Загрузить датасет», в меню загрузки скана, после чего в открывшемся меню (см. рис. 4), найти в директориях необходимый для загрузки датасет – папку, в которой содержатся DICOM файлы или файлы формата .dcm. (Перед загрузкой необходимо предобработать данные, один датасет в одной папке).

Рисунок 4. Меню загрузки датасета:

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(12).jpeg" height="200">

После выбора, необходимо нажать на кнопку соответствующую кнопку для  загрузки  КТ,  МРТ  или  УЗИ.  Для  файла  со  сканами  УЗИ

предварительно необходимо сделать предобработку с помощь Python скриптов (см. Resourses/Python_Scripts/).

Далее произойдёт загрузка датасет и его обработка, после чего будет открыт доступ к отображению датасета и к меню взаимодействия с датасетом (см. рис. 5).


Рисунок 5. Меню настройки датасета:

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(13).jpeg" height="200">


В этом меню пользователю доступны под меню.

- Ручная калибровка положения – позволяет до настроить положение и вращение датасета, с помощью слайдеров (см. рис. 6);

Рисунок 6. Меню ручной калибровки положения датасета:

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(1).jpeg" height="200">


- Пропуск света / Излучения – в этом меню пользователь может изменить

«Режимы рендера модели», в котором присутствует три режима: стандартный рендер, режим прозрачности и рендеринг изоповерхности (разделение датасета на слои поглощения излучения) и настройки яркости/коэффициента поглощения излучения, с помощью двух слайдеров (см. рис. 7);


Рисунок 7. Меню пропуска света и излучения:

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(2).jpeg" height="200">


- Объёмный срез – В этом меню, можно объёмный срез, отключить объёмный срез, восстановить значения объёмного среза по умолчанию, изменить позицию, вращение и размеры плоскости и активировать режим

  «Срез»  позволяющий  покадрово  пролистывать  датасет  по  всем осям (см. рис. 8);

Рисунок 8. Меню настройки объёмного среза:

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(3).jpeg" height="200">


- Плоский срез – В этом меню, можно создать плоскость, отключить плоскость, восстановить значения по умолчанию и изменить позицию и вращение плоскости (см. рис. 9);

Рисунок 9. Меню настройки плоского среза:

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(4).jpeg" height="200">


**Второй режим** – визуализация модели по анатомическому атласу.

В меню режима можно отключить части модели, что позволяет увидеть органы по отдельности на анатомическом атласе (см. рис. 10).

**Третий режим** – визуализация модель по анатомическим схемам.

В меню режима можно отключить части модели, что позволяет увидеть органы по отдельности на анатомических схемам (см. рис. 10).

Рисунок 10. Меню настройки визуализация модели по анатомическому атласу и схеме (два схожих меню):

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(5).jpeg" height="200">


При включение всех визуализаций, меню визуализаций будет иметь следующий вид (см. рис. 11).


Рисунок 11. Меню визуализации со всем включёнными визуализациями:

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(6).jpeg" height="200">


**Предобработка сканов УЗИ**

Для работы со схемами УЗИ необходимо предварительно их обработать с помощью Python c помощью.

Первым	этапом	имеющийся	видоряд	сканов	необходимо	перевести	в покадровый вид (см. Resourses/Python_Scripts/first.py).

Перевод полученных кадров в файлы DICOM (см. Resourses/Python_Scripts/second.py).

### **Приложение**

Изменение исходного кода MetaSDK: /Resourses/Other/SpatialAnchorSpawnerBuildingBlock

Рисунок 2.1. Настройка Meta Quest Link:

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(7).jpeg" height="300">

Рисунок 2.2. Вид скрипта в инспекторе Unity:

<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(8).jpeg" height="100">

Рисунок 2.3. Проверка работоспособности модуля «SimpleITKCSharpNative»:


<img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Instruction_Screnshots/Screnshot%20(1).png" height="200">

(Файл находится в архиве, его необходимо предварительно разорхевировать, файл находится в Resourses/Other/SimpleITKCSharpNative.7z)

### **Дополнительный скриншоты:**

<div align="center">
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/Preview.png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(1).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(2).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(3).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(4).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(5).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(6).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(7).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(8).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(9).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(10).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(11).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(12).png" height="200" />
  <img src="https://github.com/Dmitriy-OLW/AR-Project-Sezriv-Ralizonki/blob/main/Resourses/Screnshots/ScrenShot%20(13).png" height="200" />
</div>
