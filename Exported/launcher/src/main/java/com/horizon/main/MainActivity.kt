package com.horizon.main

import android.app.Activity
import android.os.Bundle
import com.UnityTechnologies.com.unity.template.urpblank.R

class MainActivity : Activity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        //        if (savedInstanceState == null) {
//            getSupportFragmentManager().beginTransaction()
//                    .replace(R.id.container, MainFragment.newInstance())
//                    .commitNow();
//        }
    }
}