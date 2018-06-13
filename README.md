# SmartPlug

## Zasada działania oraz krótki opis

**SmartPlug** jest projektem zaliczeniowym na zajęcia z Podstawy Techniki Mikroprocesorowej, napisanym w języku C. Dodatkowo, aplikacja służąca do komunikacji między użytkownikiem a urządzeniem, została napisana w C#. SmartPlug działa na prostej zasadzie:

- urządzenie elektroniczne (TV, lampka, suszarka, etc.) zostaje podłączane do 'gniazdka',

- gniazdko 'tworzy' własny hotspot - otwarta sieć **AI-THINKER_F68149**,

- można się do niego podłączyć za pomocą interfejsu aplikacji, należy również podać przykładowo Adres: 192.168.4.1 oraz nr Portu: 1234,

- dodatkowo aplikacja umożliwia przełączanie przekaźników,

- przekaźniki po przekroczeniu wymaganej wartości, przełączają się w sposób skokowy, umożliwiając dopływ prądu lub jego odcięcie, dzięki czemu urządzenie elektroniczne może się włączyć lub wyłączyć.

## Wykorzystywane podzespoły

1.	STM32F407G Discovery

![stm32f4](https://image.ibb.co/kWcQSy/stm32f4_discovery.jpg)

2.	Moduł Wi-Fi ESP8266-01

![esp](https://image.ibb.co/mrpVSy/HCMODU0085_800_600_New.jpg)

4.	Przekaźnik SRD-12VDC-SL-C

![relay](https://image.ibb.co/jzYAud/relay_module_12v_1024x1024.jpg)

## Sposób podłączenia pinów

|PORT GPIO - STM32|PORT - ESP8266-01          |PORT SRD-12VDC-SL-C|
|-|-|-|
|GND|GND|GND|
|PC10 (TX)|RX|-|
|PC11 (RX)|TX|-|
|PE12|-|INPUT1|
|PE13|-|INPUT2|
||||

## Struktura projektu i opis

> \.\.\SmartGniazdko\SmartGniazdkoSTMProjekt\main.c
>>	Lokalizacja pliku *main.c*, w którym zawarta jest cała logika projektu, realizowana na mikrokontrolerze.
>>
>>> int main(void);
>>>> Funkcja zawierająca pętlę nieskończoną, wywoływane są w niej również inne procedury oraz funkcje.
>>>
>>> void UARTSettings(void);
>>>> Procedura odpowiedzialna za poprawne ustawienie USART-a 3, w naszym przypadku -  modułu Wi-Fi ESP8266-01.
>>>
>>> void RelaysSettings(void);
>>>> Procedura, w której odpowiednio ustawiany jest przekaźnik SRD-12VDC-SL-C.
>>>
>>> void ESPInitialization(void);
>>>> Procedura, w której przy pomocy modułu Wi-Fi ESP8266-01 w pierwszej kolejności rozłączamy jakiekolwiek połączenia z Access Pointem (komenda: **AT+CWQAP**), jeśli takie istniało; następnie ustawiamy tryb hosta (komenda: **AT+CWMODE=2**); w dalszej kolejności pozwalamy na wiele połączeń (komenda: **AT+CIPMUX=1**); na sam koniec tworzymy server i przypisujemy go do portu 1234 (komenda: **AT+CIPSERVER=1,1234**). Więcej komend można znaleźć pod linkiem: [komendy AT](https://room-15.github.io/blog/2015/03/26/esp8266-at-command-reference/).
>>>
>>> void USART3_IRQHandler(void);
>>>> Procedura odpowiedzialna za obsługę przerwań USART-a 3, w której dzieje się cała 'magia'; odbierane są wszystkie komendy i wywoływana jest procedura *handleMessage*.
>>>
>>> void handleMessage(void);
>>>> Wspomniana procedura wykorzystywana jest do poprawnego odczytywania odebranych komend, na podstawie których następnie odpowiednio przełączane są przełączniki.
>>>
>>> void sendMessage(void);
>>>> Procedura, odpowiedzialna za wysyłanie komend do USART-a 3 - modułu Wi-Fi.
>>>
>>> void getRelay(void);
>>>> Procedura w której sprawdzany jest stan portów PD12 i PD13, zależnie od stanu odsyłane są różne komendy; wykorzystywana komenda: **AT+CIPSEND** z dwoma parametrami, z których pierwszy określa id wiadomości, drugi - jej długość, następnie przy pomocy procedury *sendMessage* przesyłana jest konkretna komenda...
>>>
>>> void setRelay(void);
>>>> ..., obsługiwana w *handleMessage*, która to wywołuje procedurę *setRelay* odpowiedzialną za właściwe przełączanie przekaźników na podstawie wartości parametrów: *bit1* oraz *bit2*.
>>>
>>> uint16_t strLen(char *);
>>>> Funkcja pomocnicza, zwracająca długość, podanego jako parametr, łańcucha znaków.
>>>
>>> void Delay(void);
>>>> Procedura pomocnicza, odpowiedzialna za opóźnienie.
>
> \.\.\SmartGniazdko\SmartGniazdkoWinFormsApp\Windows app\ESP
>> Lokalizacja plików aplikacji, pośredniczącej między użytkownikiem a mikrokontrolerem.
>>> TCP.cs
>>>> Plik, w którym tworzony jest nowy klient.
>>>
>>> Program.cs
>>>> Plik, w którym wywoływana jest sama aplikacja oraz jej **zaawansowany** interfejs graficzny.
>>>
>>> MAIN_WINDOW.cs
>>>> Plik, w którym zapisana jest cała logika związana z wysyłaniem wiadomości przez klienta na serwer.
>>>
>>> MAIN_WINDOW.Designer.cs
>>> Plik odpowiedzialny za cały wygląd aplikacji, wyświetlanie placeholderów, buttonów, etc.

## Zdjęcia przedstawiające efekt końcowy

1. Podłączenie modułu Wi-Fi ESP8266-01 oraz przekaźników SRD-12VDC-SL-C z płytką STM32F407G Discovery
	
![final project](https://preview.ibb.co/cjGKLJ/4.jpg)

2.	Wygląd aplikacji służącej do komunikacji użytkownik-mikrokontroler

![app](https://image.ibb.co/m49yfJ/35328010_1826042714119289_4439020950079406080_n.png)

## Credits
Michał Mirończuk: [mirooon](https://github.com/mirooon),
Albert Millert: [devmood](https://github.com/devmood)

---
MIT Licence
