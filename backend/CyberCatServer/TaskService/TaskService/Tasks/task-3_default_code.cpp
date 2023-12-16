#include <iostream> 
using namespace std; 

int main()
{
    int size = 6; 
    int numbers[size];
    
    for (int i = 0; i < size; i++) { 
        cin >> numbers[i]; 
    }

    for (int i = 0; i < size; i++) { 
        cout << numbers[i]; 
    } 
}
