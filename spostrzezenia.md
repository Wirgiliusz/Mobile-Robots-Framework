# Spostrzeżenia do inżynierki

## Tarcie w kołach
- Tarcie zmienia się w Wheel Colllider w sekcji Forward/Sideways Friction
- Zmiana Asymptote Value na wyższą powoduje mniejszy poślizg podczas hamowania, jednak wartość ta nie zwiększa się ponad wartość Extremum Value
- Zmiana Extremum Value na większą pozwala zwiększyć także Asymptote Value i jeszcze bardziej zminimalizować poślizg, jednak z obserwacji wynika, że zmiana Extremum Value powyżej 1 powoduje dziwne ruchy robota podczas ruszania (wybija go z toru prostego)
- Zmniejszanie wartości Asymptote Slip oraz Extremum Slip powoduje mniejszy poślizg kół w trakcie ruszania (?)
